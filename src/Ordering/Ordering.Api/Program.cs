using MassTransit;
using Ordering.Api.Consumers;
using Ordering.Api.Domain.Orders;
using Ordering.Api.Persistence;
using Ordering.Api.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderingDbContext>();
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<StockDecreasedConsumer>();

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
builder.Services.AddScoped<Ordering.Api.Application.Orders.Approved.Handler>();
builder.Services.AddScoped<Ordering.Api.Application.Orders.Create.Handler>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();
//app.UseHttpsRedirection();

app.MapPost("/", async (
    Ordering.Api.Application.Orders.Create.Handler handler,
    Ordering.Api.Application.Orders.Create.Command command) =>
{
    return await handler.HandleAsync(command);
});

app.Run();