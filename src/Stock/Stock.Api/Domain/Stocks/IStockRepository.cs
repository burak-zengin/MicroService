namespace Stock.Api.Domain.Stocks;

public interface IStockRepository
{
    Task CreateAsync(Stock stock);

    Task DecreaseAsync(List<Stock> stocks);
}
