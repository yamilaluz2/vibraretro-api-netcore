public interface DAOPost
{
    public void CreatePost(Post post);
    public List<Post> GetPost(int idUserLogged,int userId, int pageNumber, int pageSize,string currenView);

    public Post ExistPost (int idPost);
    Post? GetPostId(int idPost);
}