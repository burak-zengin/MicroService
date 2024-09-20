using Identity.Api.Domain.Users;
using Identity.Api.Infrustructure.Security;
using Identity.Api.Persistence;
using Identity.Api.Persistence.Repository;
using Identity.Api.Application.Users.GetToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IdentityDbContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<Handler>();
builder.Services.AddScoped<JwtGenerator>();

var app = builder.Build();
app.UseHttpsRedirection();

app.MapPost("/Token", async Task<Results<Ok<string>, BadRequest>> (
    Handler handler,
    Command command) =>
{
    var token = await handler.HandleAsync(command);

    if (string.IsNullOrEmpty(token))
    {
        return TypedResults.BadRequest();
    }

    return TypedResults.Ok(token);
});
app.Run();