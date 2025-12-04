public interface DAOFollow
{
    public void FollowUser(Follower seguidor);

    public Follower? existRelacion(int id, int idSeguidor);

    public void DeleteFollowUser(Follower idRelacion);

    public List<DTOUserFollowerResponse> buscarUsername(int userId, string? userName, string filtro, int pageNumber, int pageSize);

}