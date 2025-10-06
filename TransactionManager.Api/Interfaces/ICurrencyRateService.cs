namespace TransactionManager.Api.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<decimal> GetExchangeRateAsync(DateOnly date, string currencyCode);
    }
}
