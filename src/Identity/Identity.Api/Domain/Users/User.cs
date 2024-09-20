using Identity.Api.Domain.Roles;

namespace Identity.Api.Domain.Users;

public class User
{
    public int Id { get; set; }

    public string RoleId { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }
}
