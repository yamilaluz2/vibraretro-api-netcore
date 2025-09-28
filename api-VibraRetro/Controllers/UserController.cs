using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Internal;

namespace api_practicando.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private DAOFactory? df;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, DAOFactory df)
    {
        _logger = logger;
        this.df = df;
    }





    [HttpPost("register")]
    public IActionResult Register([FromBody] PostUserDTORequest request)
    {
        if (request.name == null &&
            request.mail == null &&
            request.userName == null &&
            request.password == null)
        {
            return BadRequest("Faltan campos obligatorios");
        }

        if (request.mail == null)
        {
            return BadRequest("falta email");
        }
        Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool IsValid = regex.IsMatch(request.mail);

        if (!IsValid)
        {
            return BadRequest("el email no cumple con los parametros");
        }

        User persona = new User
        {
            Name = request.name,
            Mail = request.mail,
            UserName = request.userName,
            Password = request.password
        };


        bool respuesta = this.df.CreateUser().create(persona);

        if (respuesta)
        {
            return Ok(new { message = "usuario creado correctamente" });

        }

        return BadRequest("Error al cargar el usuario");





















    }

    [HttpPost("login")]

    public IActionResult Login([FromBody] GetLoginRequest request)
    {
        User usuario = this.df.buscarUserMail().ExisteMail(request.mail);

        if (usuario == null)
        {
            return BadRequest(new { message = "usuario no encontrado" });
        }
        if (usuario.Password != request.password)
        {
            return BadRequest(new { error = "contraseña incorrecta" });
        }
        return Ok(new { message = "inicio de sesion correcto" });

    }




    [HttpPut("Update")]

    public IActionResult Update([FromForm] PutUserDTORequest request)
    {
        User usuario = this.df.buscarUserId().ExisteId(21);

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
            usuario.Password = request.password;
        }

        /*if (request.avatar != null)
        {
            string ruta = 
        }

        if (request.coverPhoto != null)
        {
            usuario.CoverPhoto = request.coverPhoto;
        }*/

        this.df.update().save(usuario);

        return Ok(new { message = "usuario actualiado correctamente" });

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
        request.userName == usuario.UserName && request.password == usuario.Password)
        {
            this.df.Delete().DeleteUser(usuario);
            return Ok(new { message = "usuario Eliminado correctamente" });
        }

        return BadRequest(new { message = "No se pudo eliminar el usuario" });
        
           

        

        

        


        

        
    }
    
    



};













/*User NewUser = new User
        {
            Name = request.name,
            Mail = request.mail,
            UserName = request.userName,
            Password = request.password

        };

        return Ok(new GetUserDTOResponse
        {
            nombre = NewUser.Name,
            email = NewUser.Mail
        });*/
