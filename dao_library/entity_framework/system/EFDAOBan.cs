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
}