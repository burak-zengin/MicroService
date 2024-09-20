using Identity.Api.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Persistence.Repository;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async Task<User> ReadAsync(string username, string password)
    {
        return await context.Users.AsNoTracking().Include(u => u.Role).AsSplitQuery().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
    }
}
