public class DeleteUserDTORequest
{
    public required string name { get; set; }
    public required string mail { get; set; }
    public required string userName { get; set; }
    public required string password { get; set; }

    public required int id { get; set; }
}