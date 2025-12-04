public class EFDAOFactory : DAOFactory
{
    private AppDbContext AppDbContext;

    public EFDAOFactory(AppDbContext AppDbContext)
    {
        this.AppDbContext = AppDbContext;
    }

    public DAOUser buscarRelacion()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser buscarUserId()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser buscarUserMail()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOBan CreateBan()
    {
        return new EFDAOBan(AppDbContext);
    }

    public DAOComment CreateComment()
    {
        return new EFDAOComment(AppDbContext);
    }

    public DAOPost createPost()
    {
        return new EFDAOPost(AppDbContext);
    }

    public DAOReaction createReaction()
    {
        return new EFDAOReaction(AppDbContext);
    }

    public DAOUser CreateRelationFollow()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser CreateUser()
    {
        return new EFDAOUser(AppDbContext);

    }

    public DAOUser Delete()
    {
       return new EFDAOUser(AppDbContext);
    }

    public DAOUser DeleteRelationFollow()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser filtrarUser()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser GetUsers()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser update()
    {
        return new EFDAOUser(AppDbContext);
    }
    
}