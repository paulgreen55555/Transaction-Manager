using System.Text.Json.Serialization;

namespace TransactionManager.Api.Models
{
    public class CountryCurrency
    {
        [JsonPropertyName("country_currency_desc")]
        public string? CountryCurrencyDesc { get; set; }
    }
}
