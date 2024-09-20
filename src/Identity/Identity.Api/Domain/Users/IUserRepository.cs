namespace Identity.Api.Domain.Users;

public interface IUserRepository
{
    Task<User> ReadAsync(string username, string password);
}
