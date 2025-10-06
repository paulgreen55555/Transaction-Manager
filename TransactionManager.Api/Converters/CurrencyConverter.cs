namespace TransactionManager.Api.Converters
{
    public static class CurrencyConverter
    {
        public static decimal RoundToCent(decimal value)
        {
            return Math.Round(value, 2, MidpointRounding.ToEven);
        }
    }
}
