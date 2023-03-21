using System.Text.Json.Serialization;

public class StockOperation {
    
    [JsonPropertyName("operation")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required OperationType Type { get; set; } //If the operation was a buy or sell

    [JsonPropertyName("unit-cost")]
    public required decimal UnitCost { get; set; } //The stock's unit cost using a currency with two decimal places

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; } //The quantity of stocks negotiated
}