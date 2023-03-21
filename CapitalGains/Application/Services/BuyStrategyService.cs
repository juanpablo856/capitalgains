/// <summary>
/// Strategy to handle buying operations
/// </summary>
public class BuyStrategyService : IStockOperationStrategyService
{
    /// <summary>
    /// Updates the WeightedAverage and CurrentStockQuantity in <paramref name="operationsState"/
    /// When a buy operation arrives the weighted average must be calculated since the stocks could have been 
    /// purchased at different prices. Finally, amount of stocks should be increase
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="operationsState"></param>
    public void UpdateOperationsState(StockOperation operation, OperationsState operationsState)
    {
        if (operationsState.WeightedAverage == default || operationsState.CurrentStockQuantity == default)
        {
            operationsState.WeightedAverage = operation.UnitCost;
            operationsState.ResetProfitAndLosses();
        }
        else
        {
            decimal cost = (operationsState.CurrentStockQuantity * operationsState.WeightedAverage) + (operation.Quantity * operation.UnitCost);
            int quantity = (operationsState.CurrentStockQuantity + operation.Quantity);
            operationsState.WeightedAverage = cost / quantity;
        }
        
        operationsState.CurrentStockQuantity += operation.Quantity;
    }

    /// <summary>
    /// Buying stocks do not pay any taxes
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="operationsState"></param>
    /// <returns></returns>
    public CapitalGainsTax CalculateTaxes(StockOperation operation, OperationsState operationsState)
    {
        return new CapitalGainsTax { Tax = decimal.Round(0.00m, 2, MidpointRounding.AwayFromZero) };
    }
}
