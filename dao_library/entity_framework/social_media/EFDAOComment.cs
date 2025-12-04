
public class EFDAOComment : DAOComment
{
    private AppDbContext dbContext;

    public EFDAOComment(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public void CreateComment(Comment comment)
    {
        dbContext.Comments.Add(comment);
        dbContext.SaveChanges();
        

    }

    public List<Comment> GetComment(int idPost, int pageNumber, int pageSize)
    {
        List<Comment> listComment = dbContext.Comments.Where(c => c.Post.Id == idPost )
        .OrderByDescending(c => c.Id)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        return listComment;
    }
}