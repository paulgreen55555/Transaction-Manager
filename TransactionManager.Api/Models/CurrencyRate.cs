using System.Text.Json.Serialization;

namespace TransactionManager.Api.Models
{
    public class CurrencyRate
    {
        [JsonPropertyName("exchange_rate")]
        public decimal ExchangeRate { get; set; }

        [JsonPropertyName("record_date")]
        public DateTime RecordDate { get; set; }
    }
}
