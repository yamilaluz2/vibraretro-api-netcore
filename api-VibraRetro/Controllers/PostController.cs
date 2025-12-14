using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace api_VibraRetro.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private DAOFactory df;
    private IFile image;
    private IToken tokenService;

    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger, DAOFactory df, IFile image, IToken token)
    {
        _logger = logger;
        this.df = df;
        this.image = image;
        this.tokenService = token;
    }


    [Authorize]
    [HttpPost("createPost")]
    public IActionResult createPost([FromForm] PostCreatePostDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        string host = "http://localhost:5029";
        string rute = "wwwroot/photo/post";
        string? UrlImagePost= null;

        if (request.description == null &&
            request.postImage == null)
        {
            return BadRequest("Faltan campos obligatorios");
        }

        if (request.postImage != null)
        {
            (string PathCompleto, string nombreArchivo) = this.image.GetPath(request.postImage, rute);

            try
            {
                this.image.SaveFile(request.postImage, PathCompleto);
                UrlImagePost =$"{host}/photo/post/{nombreArchivo}";
                
            }
            catch
            {
                return BadRequest(new { message = "usuario no aca encontrado" });
            }

        }

        User usuario = this.df.UserDAOFactory().ExisteId(userId); 

        Post post = new Post
        {
            Description = request.description,
            PostImage = UrlImagePost,
            Creator = usuario

        };

        this.df.PostDAOFactory().CreatePost(post);



        return Ok(new GetPostDTOResponse
        {
            idOwner = post.GetUserId(), 
            imgOwner = post.GetUserName(),
            nameOwner= post.GetAvatar(),
            body = post.Description,
            image = post.PostImage,
            countLove= post.GetCountLike(),
            countAngry = post.GetCountAngry(),
            countComments= post.GetCountComment(),
            id = post.Id
        });

    }


    [Authorize]
    [HttpGet("GetPost")]
    public IActionResult getPost([FromQuery] GetPostDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int idUserLogged = int.Parse(userIdString);

            

        List<Post> posts = this.df.PostDAOFactory().GetPost(idUserLogged,request.id,request.pageNumber,request.pageSize,request.currenView); 

        List<GetPostDTOResponse> listPost = posts.Select(post => new GetPostDTOResponse
        {
            idOwner = post.GetUserId(), 
            imgOwner = post.GetUserName(),
            nameOwner= post.GetAvatar(),
            body = post.Description,
            image = post.PostImage,
            countLove= post.GetCountLike(),
            countAngry = post.GetCountAngry(),
            countComments= post.GetCountComment(),
            id = post.Id
        }).ToList();


        return Ok(listPost);
    }


    [Authorize]
    [HttpGet("GetPostId")]
    public IActionResult getPostId([FromQuery] GetPostIdDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

            

        Post? post = this.df.PostDAOFactory().GetPostId(request.idPost);

        if (post!= null)
        {
            GetPostDTOResponse postDTO = new GetPostDTOResponse
            {
                idOwner = post.GetUserId(), 
                imgOwner = post.GetUserName(),
                nameOwner= post.GetAvatar(),
                body = post.Description,
                image = post.PostImage,
                countLove= post.GetCountLike(),
                countAngry = post.GetCountAngry(),
                countComments= post.GetCountComment(),
                id = post.Id
            };
            return Ok(postDTO); 

        }

        return BadRequest("no hay post");
        
            
    }

    [Authorize]
    [HttpPut("Update")]
    public IActionResult Update([FromForm] PutPostDTORequest request)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        string host = "http://localhost:5029";
        string rute = "wwwroot/photo/post/";

        Post post = this.df.PostDAOFactory().ExistPost(request.id);
        
        if (post == null)
        {
            return BadRequest(new PostResponseDTOCommon
            {
                success=false,
                message="Post no encontrado"
            });
        }

        if(request.description != null)
        {
            post.Description = request.description;
        }

        if(request.postImage != null)
        {
            (string PathCompleto, string nombreArchivo) = this.image.GetPath(request.postImage, rute);

            try
            {
                this.image.SaveFile(request.postImage, PathCompleto);
                post.PostImage = $"{host}/photo/post/{nombreArchivo}";

            }
            catch
            {
                return BadRequest(new PostResponseDTOCommon
                {
                    success=false,
                    message = "Error al guardar"
                });
        
                
            }
        }

        this.df.PostDAOFactory().Save(post);
        return Ok(new GetPostDTOResponse
        {
            idOwner = post.GetUserId(),
            imgOwner = post.GetUserName(),
            nameOwner = post.GetAvatar(),
            body = post.Description,
            image = post.PostImage,
            countLove = post.GetCountLike(),
            countAngry = post.GetCountAngry(),
            countComments = post.GetCountComment(),
            id = post.Id
        });

    }

    [Authorize]
    [HttpDelete("{idPost}")]
    public IActionResult Delete (int idPost)
    {
        var userIdString = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("Token inválido o sin UserId.");
        }

        int userId = int.Parse(userIdString);

        bool isDelete = this.df.PostDAOFactory().DeletePost(userId,idPost);

        if (!isDelete)
        {
            return BadRequest(new PostResponseDTOCommon
            {
                success=false,
                message="No se pudo eliminar el post. Post no encontrado"
            });
        }

        return Ok(new PostResponseDTOCommon
        {
            success=true,
            message="El post se elimino correctamente"
        });

        
        


        

        


    }


};



    