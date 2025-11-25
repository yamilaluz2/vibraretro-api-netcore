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
    [HttpPost("follow")]
    public IActionResult follow([FromBody] FollowDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        if (userId == request.id)
        {
            return Ok("no se puede seguir a uno mismo");
        }

        User usuarioSeguidor = this.df.buscarUserId().ExisteId(userId);
        User usuarioseguido = this.df.buscarUserId().ExisteId(request.id);

        Follower? existRelation = this.df.buscarRelacion().existRelacion(userId, request.id);

        if (existRelation != null)
        {
            this.df.DeleteRelationFollow().DeleteFollowUser(existRelation);
            return Ok(new Unfollowresponse
            {
                userId = request.id,
                isFollowing = false
            });

        }

        Follower seguimiento = new Follower
        {
            FollowerUser = usuarioSeguidor,
            FollowedUser = usuarioseguido
        };

        this.df.CreateRelationFollow().FollowUser(seguimiento);

        return Ok(new Unfollowresponse
            {
                userId = request.id,
                isFollowing = true
            });


    }

    [Authorize]
    [HttpGet("BuscarUserName")]
    public IActionResult buscarUser([FromQuery] GetFollowDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);
               
        var listUser = this.df.filtrarUser().buscarUsername(userId, request.userName, request.filtro, request.pageNumber, request.pageSize);

        var listaDto = listUser.Select(u => new FollowingDTOResponse
        {
            id = u.Usuario.Id,
            userName = u.Usuario.UserName,
            avatar = u.Usuario.Avatar,
            isFollowing = u.LoSigo
        }).ToList();

        return Ok(listaDto);

    }

}    