using Dapper;
using MassTransit;
using Microsoft.Data.SqlClient;
using Stock.Api.Consumers;
using Stock.Api.Domain.Stocks;
using Stock.Api.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMassTransit(configure =>
{
    configure.AddConsumer<PaymentCompletedConsumer>();
    configure.AddConsumer<ProductCreatedConsumer>();

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
builder.Services.AddScoped<Stock.Api.Application.Create.Handler>();
builder.Services.AddScoped<Stock.Api.Application.Decrease.Handler>();
builder.Services.AddScoped<IStockRepository, StockRepository>();

var app = builder.Build();
app.UseHttpsRedirection();
app.Run();