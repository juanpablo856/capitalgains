public class OperationsState {
    public decimal WeightedAverage { get; set; }
    public int CurrentStockQuantity { get; set; }
    public decimal AccruedLosses { get; set; }
    public decimal AccruedProfitBeforeTaxes { get; set; }

    public OperationsState()
    {        
    }

    public void ResetProfitAndLosses() 
    {
        AccruedProfitBeforeTaxes = default;
        AccruedLosses = default;
    }
}