public class PutUserDTORequest
{
    public string? name { get; set; }
    public string? mail { get; set; }
    public string? userName { get; set; }
    public string? password { get; set; }
    public IFormFile? avatar { get; set; }
    public IFormFile? coverPhoto { get; set;}
    public int id { get; set;}
    
}