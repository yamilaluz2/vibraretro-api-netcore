public class EFDAOFactory : DAOFactory
{
    private AppDbContext AppDbContext;

    public EFDAOFactory(AppDbContext AppDbContext)
    {
        this.AppDbContext = AppDbContext;
    }

    public DAOUser buscarUserId()
    {
        return new EFDAOUser(AppDbContext);
    }

    public DAOUser buscarUserMail()
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

    public DAOUser update()
    {
        return new EFDAOUser(AppDbContext);
    }
    
}