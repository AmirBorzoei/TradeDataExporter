using Microsoft.EntityFrameworkCore;
using TradeDataExporter.Model.LastTrades;

namespace TradeDataExporter.Data.EF;

public class LastTradeEfRepository : ILastTradeRepository
{
    private readonly ITradeDbContext _context;

    public LastTradeEfRepository(ITradeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LastTrade>> GetAsync(DateTime? startDate, CancellationToken ct)
    {
        var query = _context.LastTrades.AsQueryable();
        if (startDate.HasValue) query = query.Where(t => t.DateTimeEn > startDate);

        var lastTrades = await query.ToListAsync(ct);
        return lastTrades;
    }
}