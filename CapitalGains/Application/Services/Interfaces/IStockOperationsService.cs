public interface IStockOperationsService {

    public IEnumerable<StockOperation> ReadLine(string jsonLine);
    
    public void UpdateStockOperationsState(StockOperation operation);

    public CapitalGainsTax CalculateTaxes(StockOperation operation);

}