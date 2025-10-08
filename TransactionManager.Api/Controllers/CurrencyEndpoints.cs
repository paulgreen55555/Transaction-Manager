using TransactionManager.Api.Interfaces;

namespace TransactionManager.Api.Controllers
{
    public static class CurrencyEndpoints
    {
        public static WebApplication MapCurrencyEndpoints(this WebApplication app)
        {
            app.MapGet("api/currency", async (ICurrencyRateService currencyRateService) =>
            {
                var result = await currencyRateService.GetCurrenciesAsync();

                return Results.Ok(result);
            });

            return app;
        }
    }
}
