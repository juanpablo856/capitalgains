/// <summary>
/// Strategy to handle selling oparations
/// </summary>
public class SellStrategyService : IStockOperationStrategyService
{
    /// <summary>
    /// Updates the CurrentStockQuantity in <paramref name="operationsState"/>
    /// When a sell operation arrives the amount of stocks must be decrease 
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="operationsState"></param>
    public void UpdateOperationsState(StockOperation operation, OperationsState operationsState)
    {
        operationsState.CurrentStockQuantity -= operation.Quantity;
    }

    /// <summary>
    /// Calculates the tax generated in a selling operation, it takes into account that the accrued losses so far,
    /// need to be deducted before calculating the tax.
    /// If selling operation price is lower than the weighted average price, it means that the operation generated a lost 
    /// and do not pay any taxes, losses are accrued.
    /// The tax percentage applied on each sell operation is parametrized in "<see cref="Constants.OverallProfit"/>"
    /// and only when the overall profit is greater than "<see cref="Constants.CapitalGainMaxThreshold"/>"
    /// </summary>
    /// <param name="operation"></param>
    /// <param name="operationsState"></param>
    /// <returns>"<see cref="CapitalGainsTax"/>"</returns>
    public CapitalGainsTax CalculateTaxes(StockOperation operation, OperationsState operationsState)
    {
        var capitalGain = (operation.UnitCost * operation.Quantity) - (operationsState.WeightedAverage * operation.Quantity);
        if (capitalGain < 0)
        {
            var losses = Math.Abs(capitalGain);
            operationsState.AccruedLosses += losses;
        }
        else
        {
            operationsState.AccruedProfitBeforeTaxes += capitalGain;

            if (operationsState.AccruedProfitBeforeTaxes >= Constants.CapitalGainMaxThreshold)
            {
                var profitLessLost = operationsState.AccruedProfitBeforeTaxes - operationsState.AccruedLosses;
                if (profitLessLost > 0)
                {
                    operationsState.ResetProfitAndLosses();
                    return new CapitalGainsTax { Tax = decimal.Round(profitLessLost * Constants.OverallProfit, 2, MidpointRounding.AwayFromZero) };
                }
            }
        }
        return new CapitalGainsTax { Tax = decimal.Round(0.00m, 2, MidpointRounding.AwayFromZero) };
    }
}