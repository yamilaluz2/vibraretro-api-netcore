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

    public List<Post> GetPost(int userId, int pageNumber, int pageSize,string currenView)
    {
        if(currenView == "wall")
        {
            List<Post> listPostwall = dbContext.Posts
            .OrderByDescending(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            return listPostwall;    
        }
        
        
        
        List<Post> listPost = dbContext.Posts.Where(p => p.Creator.Id == userId )
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