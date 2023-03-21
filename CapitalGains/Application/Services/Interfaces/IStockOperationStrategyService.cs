public interface IStockOperationStrategyService
{
    void UpdateOperationsState(StockOperation operation, OperationsState operationsState);
    CapitalGainsTax CalculateTaxes(StockOperation operation, OperationsState operationsState);
}