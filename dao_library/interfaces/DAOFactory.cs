using Microsoft.EntityFrameworkCore.Migrations.Operations;

public interface DAOFactory
{
    public DAOUser UserDAOFactory();
    public DAOPost PostDAOFactory();
    public DAOFollow FollowDAOUser();
    public DAOReaction ReactionDAOFactory();
    public DAOComment CommentDAOFactory();
    public DAOBan BanDAOFactory();
    
}

