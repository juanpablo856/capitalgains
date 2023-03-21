using System.Text.Json.Serialization;
public class CapitalGainsTax {
    
    [JsonPropertyName("tax")]
    public decimal Tax { get; set; }
}