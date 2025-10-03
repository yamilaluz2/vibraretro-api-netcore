using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Internal;
using BCrypt.Net;

namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private DAOFactory df;
    private IFile image;
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, DAOFactory df, IFile image)
    {
        _logger = logger;
        this.df = df;
        this.image = image;
    }

    [HttpPost()]
    public IActionResult Register([FromBody] PostUserDTORequest request)
    {
        try
        {
            validateUser(request);
            
            User usuario = new User
            {
                Name = request.name,
                Mail = request.mail,
                UserName = request.userName
            };

            usuario.SetPassword(request.password);

            this.df.CreateUser().Create(usuario);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }

        return Ok(new { message = "usuario creado correctamente" });
    }

    private void validateUser(PostUserDTORequest request)
    {
        //TODO: Eze, poner todos con IsNullOrEmpty.
        if (String.IsNullOrEmpty(request.name)) throw new Exception("El nombre es un dato obligatorio");
        if (request.mail == null) throw new Exception("El mail es un dato obligatorio");
        if (request.userName == null) throw new Exception("El user name es un dato obligatorio");
        if (request.password == null) throw new Exception("El password es un dato obligatorio");
        
        Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool IsValid = regex.IsMatch(request.mail);

        if (!IsValid)
        {
            throw new Exception("el email no cumple con los parametros");
        }
    }

    [HttpPost("login")]

    public IActionResult Login([FromBody] GetLoginRequest request)
    {
        User usuario = this.df.buscarUserMail().ExisteMail(request.mail);

        if (usuario == null)
        {
            return BadRequest(new { message = "usuario no encontrado" });
        }
        bool resultado = usuario.VerifyPassword(request.password);
        if (!resultado)
        {
            return BadRequest(new { error = "contraseña incorrecta" });
        }
        return Ok(new { message = "inicio de sesion correcto" });
    }

    [HttpPut("Update")]
    public IActionResult Update([FromForm] PutUserDTORequest request)
    {
        User usuario = this.df.buscarUserId().ExisteId(request.id);
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
            try
            {
                string pathAvatar = this.image.Save(request.avatar, rute);
                usuario.Avatar = pathAvatar;
            }
            catch
            {
                return BadRequest(new { message = "usuario no encontrado" });
            }
        }

        if (request.coverPhoto != null)
        {
            try
            {
                string pathCoverPic = this.image.Save(request.coverPhoto, rute);
                usuario.CoverPhoto = pathCoverPic;
            }
            catch
            {
                return BadRequest(new { message = "usuario no encontrado" });
            }
        }

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
        request.userName == usuario.UserName && request.password == usuario.PasswordHash)
        {
            this.df.Delete().DeleteUser(usuario);
            return Ok(new { message = "usuario Eliminado correctamente" });
        }

        return BadRequest(new { message = "No se pudo eliminar el usuario" });       
    }
};













