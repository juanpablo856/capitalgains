using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using CapitalGains.Application.Services;

[assembly: InternalsVisibleTo("CapitalGains.Tests")]
namespace CapitalGains;

internal class Program
{
    public static void Main(string[] args)
    {
        var provider = new ServiceCollection()
                .AddTransient<BuyStrategyService>()
                .AddTransient<SellStrategyService>()
                .BuildServiceProvider();
                
        var buyStrategy = provider.GetRequiredService<BuyStrategyService>();
        var sellStrategy = provider.GetRequiredService<SellStrategyService>();
        string jsonLine;

        while (!String.IsNullOrWhiteSpace(jsonLine=Console.ReadLine()))
        {
            IStockOperationsService stockOperationsService = new StockOperationsService(buyStrategy, sellStrategy);
            IEnumerable<StockOperation> stockOperations = stockOperationsService.ReadLine(jsonLine);
            IList<CapitalGainsTax> capitalGainsTaxes = new List<CapitalGainsTax>();
            
            foreach (StockOperation operation in stockOperations) { 
                stockOperationsService.UpdateStockOperationsState(operation);
                capitalGainsTaxes.Add(stockOperationsService.CalculateTaxes(operation));                
            }

            string jsonString = JsonSerializer.Serialize(capitalGainsTaxes);
            Console.WriteLine($"{jsonString}");
        }
    }
}
