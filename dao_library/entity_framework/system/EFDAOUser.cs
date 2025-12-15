
public class EFDAOUser : DAOUser
{

    private AppDbContext dbContext;

    public EFDAOUser(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool create(User value)
    {
        dbContext.Persons.Add(value);
        dbContext.SaveChanges();
        bool isUser = dbContext.Users.Any(u => u.Mail == value.Mail);
        return isUser;


    }

    public User ExisteMail(string value)
    {
        User? usuario = dbContext.Users.FirstOrDefault(user => user.Mail == value);
        return usuario;
    }

    public User? ExisteId(int value)
    {
        User? usuario = dbContext.Users.FirstOrDefault(user => user.Id == value);
        return usuario;
    }


    public void save(User usuario)
    {
        dbContext.Users.Update(usuario);
        dbContext.SaveChanges();
    }

    public void DeleteUser(User user)
    {
        
        dbContext.Users.Remove(user);
        dbContext.SaveChanges();

    }

    public List<User> buscarUsuario(int pageNumber, int pageSize)
    {
        List<User> usuarios = dbContext.Users
            .OrderBy(u => u.UserName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        return usuarios;
    }


    public int CountUser()
    {
        int totalUser = dbContext.Users.Count();
        return totalUser;
    }

    public List<User> GetUser(int pageNumber, int pageSize, string filter)
    {
        var query = dbContext.Users.AsQueryable();

        
        if (filter == "user")
        {
            query = query.Where(u => u.RolUser == Rol.Usuario);
        }
        else if (filter == "admin")
        {
            query = query.Where(u => u.RolUser == Rol.Administrador);
        }

        var usuarios = query
            .OrderBy(u => u.UserName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return usuarios;
    }
}