public class Reaction
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private User? creator;
    public User? Creator { get { return creator; } set { creator = value; } }

    private Post? posts;
    public Post? Posts { get { return posts; } set { posts = value; } }

    


}
