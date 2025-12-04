public class EFDAOReaction : DAOReaction
{
    private AppDbContext dbContext;

    public EFDAOReaction(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    
        
public void ApplyReaction(Reaction likePost)
    {
        dbContext.Reactions.Add(likePost);
        dbContext.SaveChanges();
    }

    public void DeleteReaction(Reaction isReaction)
    {
        dbContext.Reactions.Remove(isReaction);
        dbContext.SaveChanges();
    }

    public Reaction? ExistReaction(int userId, int postId)
{
    Reaction? reaction = dbContext.Reactions
        .Where(r => r.Creator != null 
                    && r.Creator.Id == userId 
                    && r.Posts != null 
                    && r.Posts.Id == postId)
        .FirstOrDefault();

    return reaction;
}
}

