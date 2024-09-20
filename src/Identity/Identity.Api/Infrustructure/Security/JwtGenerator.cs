using Identity.Api.Domain.Users;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Api.Infrustructure.Security;

public class JwtGenerator(IConfiguration configuration)
{
    private readonly string _issuer = configuration.GetValue<string>("Jwt:Issuer");

    private readonly string _key = configuration.GetValue<string>("Jwt:Key");

    public string Generate(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var privateKey = Encoding.UTF8.GetBytes(_key);

        var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("id", user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(user.Role.Name, true.ToString()));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Issuer = _issuer,
            Audience = _issuer,
            Expires = DateTime.Now.AddMinutes(configuration.GetValue<int>("Jwt:LifeTime")),
            NotBefore = DateTime.Now,
            IssuedAt = DateTime.Now,
            Subject = claimsIdentity
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
}