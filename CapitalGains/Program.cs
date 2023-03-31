using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using CapitalGains.Application.Services;

[assembly: InternalsVisibleTo("CapitalGains.Tests")]
namespace CapitalGains;

internal class Program
{
    internal static void Main(string[] args)
    {
        var provider = new ServiceCollection()
                .AddSingleton<BuyStrategyService>()
                .AddSingleton<SellStrategyService>()
                .AddScoped<IStockOperationsService, StockOperationsService>(
                    provider => new StockOperationsService(
                        buyStrategy: provider.GetRequiredService<BuyStrategyService>(),
                        sellStrategy: provider.GetRequiredService<SellStrategyService>()
                        )
                    )
                .BuildServiceProvider();
                
        var stockOperationsService = provider.GetRequiredService<IStockOperationsService>();
        string jsonLine;

        while (!String.IsNullOrWhiteSpace(jsonLine=Console.ReadLine()))
        {
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
