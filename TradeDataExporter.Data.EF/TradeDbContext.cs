using Microsoft.EntityFrameworkCore;
using TradeDataExporter.Model.LastTrades;

namespace TradeDataExporter.Data.EF;

public class TradeDbContext : DbContext, ITradeDbContext
{
    private readonly string _connectionString;

    public TradeDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbSet<LastTrade> LastTrades { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LastTrade>().ToTable("##TempTable");

        base.OnModelCreating(modelBuilder);
    }
}