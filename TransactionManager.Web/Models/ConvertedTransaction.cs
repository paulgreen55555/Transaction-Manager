namespace TransactionManager.Web.Models
{
    public class ConvertedTransaction
    {
        public string? Description { get; set; }

        public DateOnly TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string? CurrencyCode { get; set; }

        public decimal Rate { get; set; }

        public decimal ConvertedAmount { get; set; }
    }
}
