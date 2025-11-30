public interface DAOPost
{
    public void CreatePost(Post post);
    public List<Post> GetPost(int userId, int pageNumber, int pageSize,string currenView);
}