public class PostLikeDTOResponse
{   
    public string reactionType { get; set; }
    public int countLove{get;set;}
    public int countAngry{get;set;}

    public bool userHasReacted{get;set;}
}