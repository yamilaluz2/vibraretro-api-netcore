public class Reaction
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private User? creator;
    public virtual User? Creator { get { return creator; } set { creator = value; } }

    private Post? post;
    public virtual Post? Post { get { return post; } set { post = value; } }

    private ReactionType reactionType{get;set;}
    public virtual ReactionType ReactionType{ get {return reactionType;} set {reactionType=value;}}

    


}
