public class PostCreatePostDTORequest
{
    public required string description {get;set;}

    public required IFormFile? postImage { get; set; }

}