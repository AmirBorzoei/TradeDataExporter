namespace TradeDataExporter.Model.LastTrades;

public interface ILastTradeRepository
{
    Task<IEnumerable<LastTrade>> GetAsync(DateTime? startDate, CancellationToken ct);
}