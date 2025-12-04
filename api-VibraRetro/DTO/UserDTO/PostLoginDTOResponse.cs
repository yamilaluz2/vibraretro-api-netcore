public class PostLoginDTOResponse
{
    
    public bool success { get; set; }     
    public required string message { get; set; }    
    public string? token { get; set; }      
    public int? idUser { get; set; }       

}