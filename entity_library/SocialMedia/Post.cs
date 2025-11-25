using System.Transactions;

public class Post
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private string description = "";
    public string Description { get { return description; } set { description = value; } }

    private string postImage = "";
    public string PostImage { get { return postImage; } set { postImage = value; } }

    private User? creator;
    public virtual User? Creator { get { return creator; } set { creator = value; } }

    private List<Comment>? comments;
    public virtual List<Comment>? Comments { get { return comments; } set { comments = value; } }
    
     


    
}