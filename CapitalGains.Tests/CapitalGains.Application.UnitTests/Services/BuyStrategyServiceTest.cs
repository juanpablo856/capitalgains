public class BuyStrategyServiceTest
{
    [Fact]
    public void UpdateOperationsState_WhenNoPreviousOperation_UpdatesWeightedAverage()
    {
        // Arrange
        var operation = new StockOperation { Type = OperationType.Buy, Quantity = 10, UnitCost = 20 };
        var operationsState = new OperationsState();
        
        var buyStrategy = new BuyStrategyService();

        // Act
        buyStrategy.UpdateOperationsState(operation,operationsState);

        // Assert
        Assert.Equal(operation.UnitCost, operationsState.WeightedAverage);
    }


    [Fact]
    public void UpdateOperationsState_WhenPreviousOperation_UpdatesWeightedAverage()
    {
        // Arrange
        var previousOperation = new StockOperation { Type = OperationType.Buy, Quantity = 10000, UnitCost = 10 };
        var newOperation = new StockOperation { Type = OperationType.Buy, Quantity = 5000, UnitCost = 25 };
        var operationsState = new OperationsState 
        { 
            CurrentStockQuantity = previousOperation.Quantity, 
            WeightedAverage = previousOperation.UnitCost 
        };

        var buyStrategy = new BuyStrategyService();

        // Act
        buyStrategy.UpdateOperationsState(newOperation,operationsState);

        // Assert
        Assert.Equal(15, operationsState.WeightedAverage);
    }


    [Fact]
    public void UpdateOperationsState_UpdatesCurrentStockQuantity()
    {
        // Arrange
        var operation = new StockOperation { Type = OperationType.Buy, Quantity = 10, UnitCost = 20 };
        var operationsState = new OperationsState();

        var buyStrategy = new BuyStrategyService();

        // Act
        buyStrategy.UpdateOperationsState(operation, operationsState);

        // Assert
        Assert.Equal(operation.Quantity, operationsState.CurrentStockQuantity);
    }
}