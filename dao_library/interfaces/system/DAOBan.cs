using System.Formats.Asn1;

public interface DAOBan
{
    bool CreateBanDAO(Ban UserBan);
    Ban? SearchBan(int userId);

    bool DeleteBan(Ban banUser);

    


}