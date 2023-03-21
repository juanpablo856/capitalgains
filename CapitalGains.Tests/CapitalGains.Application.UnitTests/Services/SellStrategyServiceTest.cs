public class SellStrategyServiceTest
{
    [Fact]
    public void CalculateTaxes_SellOperationGeneratesLosses_NoTaxes()
    {
        // Arrange
        var operation = new StockOperation
        {
            Type = OperationType.Sell,
            Quantity = 10,
            UnitCost = 20m
        };
        var operationsState = new OperationsState
        {
            WeightedAverage = 25m,
            AccruedLosses = 0m,
            AccruedProfitBeforeTaxes = 0m
        };
        
        var sellStrategy = new SellStrategyService();

        // Act
        var result = sellStrategy.CalculateTaxes(operation, operationsState);

        // Assert
        Assert.Equal(0m, result.Tax);
        Assert.Equal(50m, operationsState.AccruedLosses);
    }


    [Fact]
    public void CalculateTaxes_SellOperationGeneratesProfit_PayTaxesAndResetsAccrues()
    {
        // Arrange
        var operation = new StockOperation
        {
            Type = OperationType.Sell,
            Quantity = 10000,
            UnitCost = 15m
        };
        var operationsState = new OperationsState
        {
            WeightedAverage = 10m,
            AccruedLosses = 20000m,
            AccruedProfitBeforeTaxes = 1500m
        };

        var sellStrategy = new SellStrategyService();

        // Act
        var result = sellStrategy.CalculateTaxes(operation, operationsState);

        // Assert
        Assert.Equal(6300m, result.Tax);
        Assert.Equal(0m, operationsState.AccruedLosses);
        Assert.Equal(0m, operationsState.AccruedProfitBeforeTaxes);
    }


    [Fact]
    public void CalculateTaxes_OverallProfitLessThanThreshold_NoTaxes()
    {
        // Arrange
        var operation = new StockOperation
        {
            Type = OperationType.Sell,
            Quantity = 10,
            UnitCost = 40m
        };
        var operationsState = new OperationsState
        {
            WeightedAverage = 30m,
            AccruedLosses = 0m,
            AccruedProfitBeforeTaxes = 1000m
        };

        var sellStrategy = new SellStrategyService();

        // Act
        var result = sellStrategy.CalculateTaxes(operation, operationsState);

        // Assert
        Assert.Equal(0m, result.Tax);
        Assert.Equal(1100m, operationsState.AccruedProfitBeforeTaxes);
    }
}