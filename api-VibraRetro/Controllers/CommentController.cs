using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private DAOFactory df;
    private IToken tokenService;

    private readonly ILogger<CommentController> _logger;

    public CommentController(ILogger<CommentController> logger, DAOFactory df,IToken token)
    {
        _logger = logger;
        this.df = df;
        this.tokenService = token;
    }

    [Authorize]
    [HttpPost("createComment")]
    public IActionResult createPost([FromBody] PostCommentDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        if (request.description == null)
        {
            return BadRequest("Faltan campos obligatorios");
        }

        User usuario = this.df.buscarUserId().ExisteId(userId);
        Post post = this.df.createPost().ExistPost(request.idPost);
        
        if (usuario != null && post != null){

            Comment comment = new Comment{
                Creator=usuario,
                Description=request.description,
                Post= post
                
            };
            this.df.CreateComment().CreateComment(comment);
            return Ok(new PostCommentDTOResponse
            {
                idOwner= comment.GetUserId(), 
                imgOwner= comment.GetAvatar(),
                nameOwner= comment.GetUserName(),
                body= comment.Description, 
                id = comment.Id,
                comentado=true
            } );
        }

        

        return BadRequest("usuario o post no encontrados");
  

    }

    [Authorize]
    [HttpGet("getComment")]
    public IActionResult getComment([FromQuery] GetCommentDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        List<Comment> comments = this.df.CreateComment().GetComment(request.idPost, request.pageNumber, request.pageSize); 

        List<GetCommentDTOResponse> listComment = comments.Select(comment => new GetCommentDTOResponse
        {
            idOwner= comment.GetUserId(), 
            imgOwner= comment.GetAvatar(),
            nameOwner= comment.GetUserName(),
            body= comment.Description, 
            id = comment.Id

        }).ToList();


        return Ok(listComment);
    }




    
}
