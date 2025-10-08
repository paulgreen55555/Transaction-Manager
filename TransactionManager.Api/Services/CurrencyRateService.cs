using TransactionManager.Api.Exceptions;
using TransactionManager.Api.Interfaces;
using TransactionManager.Api.Models;

namespace TransactionManager.Api.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly HttpClient _httpClient;

        public CurrencyRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> GetExchangeRateAsync(DateOnly transactionDate, string currencyCode)
        {
            var date = transactionDate.AddMonths(-6).ToString("yyyy-MM-dd");

            string url = $"https://api.fiscaldata.treasury.gov/services/api/fiscal_service/v1/accounting/od/rates_of_exchange?" +
                $"fields=exchange_rate,record_date&" +
                $"filter=country_currency_desc:eq:{currencyCode},record_date:gte:{date}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request to currency api failed with status code {response.StatusCode}");
            }

            var currencyData = await response.Content.ReadFromJsonAsync<CurrencyData>();

            if (currencyData is null || currencyData.Data is null || !currencyData.Data.Any())
            {
                throw new NotFoundException($"No data for {currencyCode} currency");
            }

            return currencyData.Data
                    .OrderByDescending(d => d.RecordDate)
                    .First()
                    .ExchangeRate;
        }

        public async Task<List<string>> GetCurrenciesAsync()
        {
            var url = "https://api.fiscaldata.treasury.gov/services/api/fiscal_service/v1/accounting/od/rates_of_exchange?fields=country_currency_desc&page[size]=265";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request to currency api failed with status code {response.StatusCode}");
            }

            var currencyData = await response.Content.ReadFromJsonAsync<CountryCurrencyData>();

            if (currencyData is null || currencyData.Data == null)
            {
                return new List<string>();
            }

            var currencyList = currencyData.Data
                .Select(x => x.CountryCurrencyDesc)
                .Distinct()
                .ToList();

            return currencyList;
        }
    }
}
