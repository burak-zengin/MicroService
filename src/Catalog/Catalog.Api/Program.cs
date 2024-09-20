using Catalog.Api.Consumers;
using Catalog.Api.Domain.Products;
using Catalog.Api.Persistence;
using Catalog.Api.Persistence.Repositories;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<StockCreatedConsumer>();

    configure.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host("rabbitmq", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        configurator.ConfigureEndpoints(context);
    });
});
builder.Services.AddDbContext<CatalogDbContext>();
builder.Services.AddScoped<Catalog.Api.Application.Products.Create.Handler>();
builder.Services.AddScoped<Catalog.Api.Application.Products.Approve.Handler>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();
//app.UseHttpsRedirection();

app.MapPost("/", async (
    Catalog.Api.Application.Products.Create.Handler handler,
    Catalog.Api.Application.Products.Create.Command command) =>
{
    return await handler.HandleAsync(command);
});

app.Run();