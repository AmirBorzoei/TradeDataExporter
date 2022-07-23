using System.Data.SqlClient;
using TradeDataExporter.Model.LastTrades;

namespace TradeDataExporter.Data.Ado;

public class LastTradeAdoRepository : ILastTradeRepository
{
    private readonly string _connectionString;

    public LastTradeAdoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<LastTrade>> GetAsync(DateTime? startDate, CancellationToken ct)
    {
        var lastTrades = new List<LastTrade>();

        await using var connection = new SqlConnection(_connectionString);

        var command = new SqlCommand("SELECT * FROM ##TempTable", connection);
        if (startDate.HasValue)
        {
            command.CommandText += " WHERE DateTimeEn >= @StartDate";
            command.Parameters.AddWithValue("@StartDate", startDate.Value);
        }

        await connection.OpenAsync(ct);
        var reader = await command.ExecuteReaderAsync(ct);
        while (await reader.ReadAsync(ct))
        {
            var lastTrade = new LastTrade
            {
                Id = Convert.ToInt32(reader["Id"]),
                InstrumentId = Convert.ToInt32(reader["InstrumentId"]),
                Shortname = reader["Shortname"].ToString()!,
                DateTimeEn = Convert.ToDateTime(reader["DateTimeEn"]),
                Open = Convert.ToDecimal(reader["Open"]),
                High = Convert.ToDecimal(reader["High"]),
                Low = Convert.ToDecimal(reader["Low"]),
                Close = Convert.ToDecimal(reader["Close"])
            };
            lastTrades.Add(lastTrade);
        }

        await reader.CloseAsync();

        return lastTrades;
    }
}