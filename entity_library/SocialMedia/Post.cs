using System.Transactions;

public class Post
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private string description = "";
    public string Description { get { return description; } set { description = value; } }

    private string postImage = "";
    public string PostImage { get { return postImage; } set { postImage = value; } }

    private  User creator=null!;
    public required virtual User Creator { get { return creator; } set { creator = value; } }

    private List<Comment>? comments;
    public virtual List<Comment>? Comments { get { return comments; } set { comments = value; } }

    private List<Reaction>? reactions;
    public virtual List<Reaction>? Reactions { get { return reactions; } set { reactions = value; } }

    public int GetUserId()
    {
        if (this.Creator.Id == null)
        {
            return 0;
        }
        return this.Creator.Id;
    }

    public string GetUserName()
    {
        return this.Creator.UserName;
    }

    public string GetAvatar()
    {
        string? avatar = this.Creator.Avatar;
        if (avatar == null)
        {
            return "";
        }
        return avatar;
    }


    public int GetCount( ReactionType reactionType)
    {
        if (this.Reactions == null)
        return 0;

        return this.Reactions.Count(r => r.ReactionType == reactionType);
    }

    public int GetCountLike()
    {
        if (this.Reactions == null)
        return 0;

        return this.Reactions.Count(r => r.ReactionType == ReactionType.Like);
    }

    public int GetCountAngry()
    {
        if (this.Reactions == null)
        return 0;

        return this.Reactions.Count(r => r.ReactionType == ReactionType.Angry);
    }

    public int GetCountComment()
    {
        if (this.Comments == null)
        return 0;

        return this.Comments.Count();
    }
    
     


    
}