using System.Text.Json;
using TradeDataExporter.Data.Ado;
using TradeDataExporter.Data.EF;
using TradeDataExporter.Model.LastTrades;

const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=TradeDB;Integrated Security=True";

DateTime? startDate = null;
if (args.Length > 0 && DateTime.TryParse(args[0], out var tempStartDate)) startDate = tempStartDate;

Console.WriteLine("Fetching Data ...");
//Use Ado Repository
ILastTradeRepository lastTradeRepository = new LastTradeAdoRepository(connectionString);
//Use EF Repository
// var tradeDbContext = new TradeDbContext(connectionString);
// ILastTradeRepository lastTradeRepository = new LastTradeEfRepository(tradeDbContext);

var lastTrades = await lastTradeRepository.GetAsync(startDate, CancellationToken.None);
Console.WriteLine("Data Fetched.");

var serializedLastTrades = JsonSerializer.Serialize(lastTrades);

Console.WriteLine("Writing Data to File ...");
var fileName = $"LastTrades_{DateTime.Now:HH-mm-ss}.txt";
await File.WriteAllTextAsync(fileName, serializedLastTrades, CancellationToken.None);
Console.WriteLine($"Data Was Written to File. ({fileName})");

Console.ReadKey();