public class Comment
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private User? creator;
    public User? Creator { get { return creator; } set { creator = value; } }
    
    private string description = "";
    public string Description { get { return description; } set { description = value; } }
}