public class EFDAOPost : DAOPost
{
    private AppDbContext dbContext;

    public EFDAOPost(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void CreatePost(Post post)
    {
        dbContext.Posts.Add(post);
        dbContext.SaveChanges();


    }

    public Post ExistPost(int idPost)
    {
        Post? post = dbContext.Posts.FirstOrDefault(post => post.Id == idPost);
        return post;
    }

    public List<Post> GetPost(int idUserLogged, int userId, int pageNumber, int pageSize, string currenView)
    {
        if (currenView == "wall")
        {
            var posts = dbContext.Posts
                .Where(p => p.Creator.Id == idUserLogged
                    || dbContext.Followers
                        .Where(f => f.FollowerUser.Id == idUserLogged)
                        .Select(f => f.FollowedUser.Id)
                        .Contains(p.Creator.Id)
            )
            .OrderByDescending(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            return posts;
        }
        if (userId != idUserLogged)
        {
            var posts = dbContext.Posts
                .Where(p => p.Creator.Id == userId &&
                            dbContext.Followers.Any(f =>
                                f.FollowerUser.Id == idUserLogged &&
                                f.FollowedUser.Id == userId))
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return posts;
        }
        


        List<Post> listPost = dbContext.Posts.Where(p => p.Creator.Id == userId)
        .OrderByDescending(p => p.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        return listPost;


    }

    public Post? GetPostId(int idPost)
    {
        Post? post = dbContext.Posts.FirstOrDefault(post => post.Id == idPost);
        return post;
    }
}