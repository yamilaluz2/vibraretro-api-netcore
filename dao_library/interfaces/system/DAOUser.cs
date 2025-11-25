public interface DAOUser
{
    public bool create(User value);
    public User ExisteMail(string value);

    public User ExisteId(int value);

    public void save(User value);

    public void DeleteUser(User value);

    public List<User> buscarUsuario(int pageNumber, int pageSize);

    public void FollowUser(Follower seguidor);

    public Follower? existRelacion(int id, int idSeguidor);

    public void DeleteFollowUser(Follower idRelacion);

    public List<DTOUserFollowerResponse> buscarUsername(int userId, string? userName, string filtro, int pageNumber, int pageSize);

    



    



    


    
    


    
}