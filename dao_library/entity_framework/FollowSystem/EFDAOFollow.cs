public class EFDAOFollow : DAOFollow
{

    private AppDbContext dbContext;

    public EFDAOFollow(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    public void FollowUser( Follower seguidor)
    {
        dbContext.Followers.Add(seguidor);
        dbContext.SaveChanges();
    }

    public Follower? existRelacion(int id, int idSeguidor)
    {
        Follower? relacion = dbContext.Followers.Where(r => r.FollowerUser != null && r.FollowerUser.Id== id && r.FollowedUser != null && r.FollowedUser.Id == idSeguidor).FirstOrDefault();
        return relacion;
    }

    public void DeleteFollowUser(Follower idRelacion)
    {
        dbContext.Followers.Remove(idRelacion);
        dbContext.SaveChanges();
    }


    public List<DTOUserFollowerResponse> buscarUsername(int userId, string? userName, string filtro, int pageNumber, int pageSize)
    {
        var query = dbContext.Users.AsQueryable();

        if (!string.IsNullOrEmpty(userName))
        query = query.Where(u => u.UserName.StartsWith(userName));

            

        if (filtro == "following")
        {
            query = query.Where(u =>
                dbContext.Followers.Any(f => f.FollowerUser.Id == userId && f.FollowedUser.Id == u.Id));
        }

        if (filtro == "followers")
        {
            query = query.Where(u =>
                dbContext.Followers.Any(f => f.FollowedUser.Id == userId && f.FollowerUser.Id == u.Id));
        }

        if (filtro == "mutual")
        {
            query = query.Where(u =>
                dbContext.Followers.Any(f => f.FollowerUser.Id == userId && f.FollowedUser.Id == u.Id) &&
                dbContext.Followers.Any(f => f.FollowedUser.Id == userId && f.FollowerUser.Id == u.Id));
        }

        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return query.Select(u => new DTOUserFollowerResponse
        {
            Usuario = u,
            LoSigo = dbContext.Followers.Any(f => f.FollowerUser.Id == userId && f.FollowedUser.Id == u.Id)
        }).ToList();

    }

}

