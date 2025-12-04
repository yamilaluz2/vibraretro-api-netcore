public class Comment
{
    private Int32 id;
    public Int32 Id { get { return id; } set { id = value; } }

    private User? creator;
    public virtual User? Creator { get { return creator; } set { creator = value; } }
    
    private string description = "";
    public string Description { get { return description; } set { description = value; } }

    private Post? post;
    public virtual Post? Post { get { return post; } set { post = value; } }

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
        return this.Creator.Avatar;
    }
}