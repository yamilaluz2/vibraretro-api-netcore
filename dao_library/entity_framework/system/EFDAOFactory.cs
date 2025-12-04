public class EFDAOFactory : DAOFactory
{
    private AppDbContext AppDbContext;

    public EFDAOFactory(AppDbContext AppDbContext)
    {
        this.AppDbContext = AppDbContext;
    }

    public DAOPost PostDAOFactory()
    {
        return new EFDAOPost(AppDbContext);
    }


    public DAOUser UserDAOFactory()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOFollow FollowDAOUser()
    {
        return new EFDAOFollow(AppDbContext);
    }

    public DAOReaction ReactionDAOFactory()
    {
        return new EFDAOReaction(AppDbContext);
    }

    public DAOComment CommentDAOFactory()
    {
        return new EFDAOComment(AppDbContext);
    }

    public DAOBan BanDAOFactory()
    {
        return new EFDAOBan(AppDbContext);
    }

    
}