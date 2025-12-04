public class Reaction
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private User? creator;
    public virtual User? Creator { get { return creator; } set { creator = value; } }

    private Post? posts;
    public virtual Post? Posts { get { return posts; } set { posts = value; } }

    private ReactionType reactionType{get;set;}
    public virtual ReactionType ReactionType{ get {return reactionType;} set {reactionType=value;}}

    


}
