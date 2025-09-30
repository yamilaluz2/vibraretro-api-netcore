public class EFDAOUser : DAOUser
{

    private AppDbContext dbContext;

    public EFDAOUser(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool create(User value)
    {
        dbContext.Users.Add(value);
        dbContext.SaveChanges();
        return true;


    }

    public User ExisteMail(string value)
    {
        User? usuario = dbContext.Users.FirstOrDefault(user => user.Mail == value);
        return usuario;
    }

    public User ExisteId(int value)
    {
        User? usuario = dbContext.Users.FirstOrDefault(user => user.Id == value);
        return usuario;
    }


    public void save(User usuario)
    {
        dbContext.Users.Update(usuario);
        dbContext.SaveChanges();
    }

    public void DeleteUser(User value)
    {
        dbContext.Users.Remove(value);
        dbContext.SaveChanges();
        
    }
}