using Identity.Api.Domain.Users;
using Identity.Api.Infrustructure.Security;

namespace Identity.Api.Application.Users.GetToken;

public class Handler(IUserRepository repository, JwtGenerator jwtGenerator)
{
    public async Task<string> HandleAsync(Command command)
    {
        var user = await repository.ReadAsync(command.Username, command.Password);
        if (user is null)
        {
            return string.Empty;
        }

        return jwtGenerator.Generate(user);
    }
}
