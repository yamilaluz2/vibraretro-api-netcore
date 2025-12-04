public class PostBanDTORequest
{
    public int userId { get; set; }
    public string reason { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
}