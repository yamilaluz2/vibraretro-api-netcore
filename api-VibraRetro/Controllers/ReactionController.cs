using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class ReactionController : ControllerBase
{
    private DAOFactory df;
    private IToken tokenService;

    private readonly ILogger<ReactionController> _logger;

    public ReactionController(ILogger<ReactionController> logger, DAOFactory df, IToken token)
    {
        _logger = logger;
        this.df = df;
        this.tokenService = token;
    }


    [Authorize]
    [HttpPost("Like")]
    public IActionResult Like([FromBody] PostLikeDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        if (!Enum.TryParse<ReactionType>(request.reactionType, true, out var reactionType))
        {
            return BadRequest("Tipo de reaccion inválida.");
        }

        User usuario = this.df.buscarUserId().ExisteId(userId);
        if (usuario == null)
        {
            return BadRequest("usuario no encontrado");
        }
               
        Post post = df.createPost().ExistPost(request.idPost);
        if (post == null)
        {
            return BadRequest("no Existe post");
        }

        Reaction? existingReaction = df.createReaction().ExistReaction(userId,request.idPost);

        if(existingReaction != null)
        {

            if (existingReaction.ReactionType == reactionType)
            {
                df.createReaction().DeleteReaction(existingReaction);

                return Ok(new PostLikeDTOResponse
                {
                    reactionType = reactionType.ToString(),
                    countLove = post.GetCountLike(),
                    countAngry = post.GetCountAngry(),
                    userHasReacted = false
                });
            }

        
            df.createReaction().DeleteReaction(existingReaction);
            
        }

        Reaction likePost = new Reaction
            {
                Creator = usuario,
                Posts = post,
                ReactionType= reactionType
            };

        df.createReaction().ApplyReaction(likePost);
        
        return Ok(new PostLikeDTOResponse
            {
                reactionType = reactionType.ToString(),
                countLove = post.GetCountLike(),
                countAngry = post.GetCountAngry()
            });     

    }


    
}