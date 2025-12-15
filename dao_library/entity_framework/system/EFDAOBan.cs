public class EFDAOBan:DAOBan
{
   private AppDbContext dbContext;

    public EFDAOBan(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public bool CreateBanDAO(Ban UserBan)
    {
        dbContext.Bans.Add(UserBan);
        dbContext.SaveChanges();
        bool isBan = dbContext.Bans.Any(ban => ban.User.Id == UserBan.User.Id);
        return isBan;
    }

    public bool DeleteBan(Ban banUser)
    {
        dbContext.Bans.Remove(banUser);
        dbContext.SaveChanges();
        bool isDelete= dbContext.Bans.Any(b => b.User.Id == banUser.User.Id);
        return !isDelete;
    }

    public Ban? SearchBan(int userId)
    {
        Ban? ban = dbContext.Bans.FirstOrDefault(b => b.User.Id == userId);
        return ban;
    }
}