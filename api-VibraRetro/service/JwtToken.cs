using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtToken : IToken
{
    private readonly IConfiguration config;

    public JwtToken(IConfiguration config)
    {
        this.config = config;
    }
    public string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RolUser.ToString())
        };

        string? secretKey = this.config["Jwt:Key"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("La clave JWT no está configurada.");
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        string? expirationValue = this.config["Jwt:ExpireMinutes"];

        if (string.IsNullOrEmpty(expirationValue))
        {
            throw new InvalidOperationException("No se encuentra el tiempo de expiracion");
        }

        int expirationMinutes = int.Parse(expirationValue);
        DateTime expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: this.config["Jwt:Issuer"],
            audience: this.config["Jwt:Audience"],
            claims: claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}