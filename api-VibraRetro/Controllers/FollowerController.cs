using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing.Internal;
using BCrypt.Net;

namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowerController : ControllerBase
{
    private DAOFactory df;
    private IFile image;
    private IToken tokenService;
    private readonly ILogger<FollowerController> _logger;

    public FollowerController(ILogger<FollowerController> logger, DAOFactory df, IFile image, IToken token)
    {
        _logger = logger;
        this.df = df;
        this.image = image;
        this.tokenService = token;
    }

    [Authorize]
    [HttpGet("getUser")]
    public IActionResult GetUser()
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        List<User> listUser = df.GetUsers().buscarUsuario();

        return Ok(listUser);

        

    }

}    