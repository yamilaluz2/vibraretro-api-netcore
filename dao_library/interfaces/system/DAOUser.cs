public interface DAOUser
{
    public bool create(User value);
    public User ExisteMail(string value);

    public User ExisteId(int value);

    public void save(User value);

    public void DeleteUser(User value);

    public List<User> buscarUsuario(int pageNumber, int pageSize);

    int CountUser(int userId);
    List<User> GetUser(int pageNumber, int pageSize);
}