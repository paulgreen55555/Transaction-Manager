namespace TransactionManager.Api.DTOs
{
    public class TransactionDTO
    {
        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
