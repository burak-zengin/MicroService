using Dapper;
using Microsoft.Data.SqlClient;
using Stock.Api.Domain.Stocks;

namespace Stock.Api.Persistence.Repositories;

public class StockRepository : IStockRepository
{
    private readonly string _connectionString;

    public StockRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Sql");
    }

    public async Task CreateAsync(Domain.Stocks.Stock stock)
    {
        using SqlConnection sqlConnection = new(_connectionString);
        await sqlConnection.ExecuteAsync("Insert Into Stocks Values (@Barcode, @Quantity)", new
        {
            stock.Barcode,
            stock.Quantity
        });
    }

    public async Task DecreaseAsync(List<Domain.Stocks.Stock> stocks)
    {
        using SqlConnection sqlConnection = new(_connectionString);
        await sqlConnection.OpenAsync();

        using var sqlTransaction = await sqlConnection.BeginTransactionAsync();
        foreach (var loopStock in stocks)
        {
            sqlConnection.Execute("Update Stocks Set Quantity -= @Quantity Where Barcode = @Barcode", new
            {
                loopStock.Barcode,
                loopStock.Quantity
            }, sqlTransaction);
        }

        await sqlTransaction.CommitAsync();
    }
}
