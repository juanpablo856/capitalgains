using System.Text.Json;

namespace CapitalGains.Application.Services;

public class StockOperationsService : IStockOperationsService
{
    private OperationsState _operationsState;
    private readonly IStockOperationStrategyService _buyStrategy;
    private readonly IStockOperationStrategyService _sellStrategy;

    public StockOperationsService(IStockOperationStrategyService buyStrategy, IStockOperationStrategyService sellStrategy)
    {
        _operationsState = new OperationsState();
        _buyStrategy = buyStrategy;
        _sellStrategy = sellStrategy;
    }

    /// <summary>
    /// Read json array line from stdin
    /// </summary>
    /// <param name="jsonLine"></param>
    /// <returns></returns>
    public IEnumerable<StockOperation> ReadLine(string jsonLine)
    {
        return JsonSerializer.Deserialize<IEnumerable<StockOperation>>(jsonLine);
    }

    /// <summary>
    /// Update stock operations state
    /// </summary>
    /// <param name="operation"></param>
    public void UpdateStockOperationsState(StockOperation operation)
    {
        if(operation.Type.Equals(OperationType.Buy))
        {
            _buyStrategy.UpdateOperationsState(operation, _operationsState);
        }
        else
        {
            _sellStrategy.UpdateOperationsState(operation, _operationsState);
        }
    }

    /// <summary>
    /// Calculates the taxes generated after do a stock trade base on type of operation 
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public CapitalGainsTax CalculateTaxes(StockOperation operation)
    {
        if(operation.Type.Equals(OperationType.Buy))
        {
            return _buyStrategy.CalculateTaxes(operation, _operationsState);
        }
        else
        {
            return _sellStrategy.CalculateTaxes(operation, _operationsState);
        }
    }
}
