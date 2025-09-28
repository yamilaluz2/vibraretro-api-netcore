public class User : Person
{
    private string userName = "";
    public string UserName { get { return userName; } set { userName = value; } }

    private string password = "";
    public  string Password { get { return password; } set { password = value; } }

    private string? avatar = "";
    public string? Avatar { get { return avatar; } set { avatar = value; } }

    private string? coverPhoto = "";
    public string? CoverPhoto { get { return coverPhoto; } set { coverPhoto = value; } }

    private Rol? rolUser;

    public Rol? RolUser{get { return rolUser; } set{ rolUser = value; }}



}

    


