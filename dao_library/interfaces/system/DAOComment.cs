
public interface DAOComment
{
    public void CreateComment(Comment comment);
    List<Comment> GetComment(int idPost, int pageNumber, int pageSize);
}