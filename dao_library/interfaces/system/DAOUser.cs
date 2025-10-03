public interface DAOUser
{
    public void Create(User value);
    public User ExisteMail(string value);

    public User ExisteId(int value);

    public void save(User value);

    public void DeleteUser(User value);


    
}