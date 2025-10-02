public class User : Person
{
    private string userName = "";
    public string UserName { get { return userName; } set { userName = value; } }

    private string passwordHash = "";
    public string PasswordHash { get { return passwordHash; } private set { passwordHash = value; } }

    private string? avatar = "";
    public string? Avatar { get { return avatar; } set { avatar = value; } }

    private string? coverPhoto = "";
    public string? CoverPhoto { get { return coverPhoto; } set { coverPhoto = value; } }

    private Rol? rolUser;

    public Rol? RolUser { get { return rolUser; } set { rolUser = value; } }



    public void SetPassword(string password)
    {
        this.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }



}

    


