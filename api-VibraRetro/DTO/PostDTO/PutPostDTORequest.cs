public class PutPostDTORequest
{
    public int id {get;set;}
    public string? description{get;set;}

    public IFormFile? postImage{get;set;}
}