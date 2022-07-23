using Microsoft.EntityFrameworkCore;
using TradeDataExporter.Model.LastTrades;

namespace TradeDataExporter.Data.EF;

public interface ITradeDbContext
{
    DbSet<LastTrade> LastTrades { get; set; }
}