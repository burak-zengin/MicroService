namespace Identity.Api.Application.Users.GetToken;

public record Command(string Username, string Password);
