using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Internal;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;

namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private DAOFactory df;
    private IFile image;
    private IToken tokenService;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, DAOFactory df, IFile image, IToken token)
    {
        _logger = logger;
        this.df = df;
        this.image = image;
        this.tokenService = token;
    }





    [HttpPost("register")]
    public IActionResult Register([FromBody] PostUserDTORequest request)
    {
        if (request.name == null &&
            request.mail == null &&
            request.userName == null &&
            request.password == null)
        {
            return BadRequest(new CommonDTOResponse
            {
                success= false,
                message = "Faltan campos obligatorios"
            });
        }

        if (request.mail == null)
        {
            return BadRequest(new CommonDTOResponse
            {
                success= false,
                message = "Falta el mail"
            });
        }
        Regex regex = new Regex(@"^[^@\s]+@[^@\s\.]+(\.[^@\s\.]+)+$");
        bool IsValid = regex.IsMatch(request.mail);

        if (!IsValid)
        {
            return BadRequest(new CommonDTOResponse
            {
                success= false,
                message = "el email no cumple con los parametros"
            });
            
        }

        Regex regexPassword = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_\-+=\[\]{};:'"",.<>/?\\|`~]).{8,}$");
        bool IsValidPassword = regexPassword.IsMatch(request.password);

        if(!IsValidPassword)
        {
            return BadRequest(new CommonDTOResponse
            {
                success= false,
                message = "La contraseña no cumple con los parámetros:\n" +
                "- Mínimo 8 caracteres\n" +
                "- Al menos una letra mayúscula\n" +
                "- Al menos un número\n" +
                "- Al menos un símbolo"
            });
        }
        Rol rol = new Rol();
        

        User usuario = new User
        {
            Name = request.name,
            Mail = request.mail,
            UserName = request.userName,
            RolUser = rol

        };

        usuario.SetPassword(request.password);

        Console.WriteLine($"Hash guardado: {usuario.PasswordHash}");

        bool respuesta = this.df.CreateUser().create(usuario);

        if (respuesta)
        {
            return Ok(new CommonDTOResponse
            {
                success= true,
                message="¡Listo! Tu cuenta ya vibra en modo retro."
            });

        }

        return BadRequest("Error al cargar el usuario");


    }
    

    [HttpPost("login")]

    public IActionResult Login([FromBody] PostLoginDTORequest request)
    {
        User usuario = this.df.buscarUserMail().ExisteMail(request.mail);

        if (usuario == null)
        {
            return Unauthorized(new PostLoginDTOResponse
            {
                success = false,
                message= "Usuario incorrecto",
                token = null,
                idUser=null
            });
        }
        bool resultado = usuario.VerifyPassword(request.password);
        if (!resultado)
        {
            return Unauthorized(new PostLoginDTOResponse
            {
                success = false,
                message= "Contraseña incorrecta",
                token = null,
                idUser=null
            });
        }
        string tokenUser = this.tokenService.GenerateToken(usuario);

        if (usuario.State)
        {
            return BadRequest(new CommonDTOResponse
                {
                    success= false,
                    message= "El Usuario se encuentra Bloqueado"
                });
        }
        
        return Ok(new PostLoginDTOResponse
        {
            success= true,
            message="login exitoso",
            token = tokenUser,
            idUser = usuario.Id
        });
       
    }



    [Authorize]
    [HttpPut("Update")]

    public IActionResult Update([FromForm] PutUserDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        User usuario = this.df.buscarUserId().ExisteId(userId);
        string host = "http://localhost:5029";
        string rute = "wwwroot/uploads";

        if (usuario == null)
        {
            return BadRequest(new { message = "usuario no encontrado" });
        }


        if (request.name != null)
        {
            usuario.Name = request.name;
        }

        if (request.mail != null)
        {
            usuario.Mail = request.mail;
        }

        if (request.userName != null)
        {
            usuario.UserName = request.userName;
        }

        if (request.password != null)
        {                       
            usuario.SetPassword(request.password);
        }

        if (request.avatar != null)
        {        
            (string PathCompleto, string nombreArchivo) = this.image.GetPath(request.avatar, rute);

            try
            {
                this.image.SaveFile(request.avatar, PathCompleto);
                usuario.Avatar =$"{host}/uploads/{nombreArchivo}";
                
            }
            catch
            {
                return BadRequest(new { message = "usuario no encontrado" });
            }
        }

        if (request.coverPhoto != null)
        {
            (string PathCompleto, string nombreArchivo) = this.image.GetPath(request.coverPhoto, rute);
            try
            {
                this.image.SaveFile(request.coverPhoto, PathCompleto);
                usuario.CoverPhoto =$"{host}/uploads/{nombreArchivo}";
            }
            catch
            {
                return BadRequest(new { message = "usuario no encontrado" });
            }
        }

        this.df.update().save(usuario);

        return Ok(new getUserProfileDTOResponse
            {
            userName = usuario.UserName,
            avatar = usuario.Avatar,
            coverPhoto= usuario.CoverPhoto
            });

    }

    [HttpPut("Delete")]

    public IActionResult Delete([FromBody] DeleteUserDTORequest request)
    {
        User usuario = this.df.buscarUserId().ExisteId(request.id);

        if (usuario == null)
        {
            return BadRequest(new { message = "usuario no encontrado" });
        }


        if (request.name == usuario.Name && request.mail == usuario.Mail &&
        request.userName == usuario.UserName && request.password == usuario.PasswordHash)
        {
            this.df.Delete().DeleteUser(usuario);
            return Ok(new { message = "usuario Eliminado correctamente" });
        }

        return BadRequest(new { message = "No se pudo eliminar el usuario" });

    }

    [Authorize]
    [HttpGet("miProfile")]

    public IActionResult miProfile([FromQuery] int data)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        User usuario = this.df.buscarUserId().ExisteId(userId);
        

        return Ok(new getUserProfileDTOResponse
        {
            userName = usuario.UserName,
            avatar = usuario.Avatar,
            coverPhoto= usuario.CoverPhoto
        });

    }


    [Authorize]
    [HttpGet("GetcountUser")]

    public IActionResult GetcountUser ()
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        int countUser = this.df.buscarUserId().CountUser(userId);
        

        return Ok(new GetCountUserDTOResponse
        {
            count = countUser
        });

    }


    [Authorize]
    [HttpGet("GetUser")]

    public IActionResult GetUser ([FromQuery] GetUserDashboardDTOrequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        int countUser = this.df.buscarUserId().CountUser(userId);

        int pageSize = 10;
        

        List<User> listUser= this.df.buscarUserId().GetUser(request.pageNumber,pageSize);

        var listaDTO = listUser.Select(u => new GetUserDashboardDTOResponse
        {
            id = u.Id,
            name = u.Name,
            userName = u.UserName,
            mail = u.Mail,
            state = u.State

        }).ToList();

        return Ok(listaDTO);


    }


    

    

};













