public interface DAOReaction
{
    public void ApplyReaction(Reaction likePost);
    public Reaction? ExistReaction(int userId,int idPost);

    public void DeleteReaction(Reaction isReaction);
}