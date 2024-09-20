using MassTransit;
using Payment.Api.Consumers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<OrderCreatedConsumer>();

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
builder.Services.AddScoped<Payment.Api.Application.Pay.Handler>();

var app = builder.Build();
app.UseHttpsRedirection();
app.Run();