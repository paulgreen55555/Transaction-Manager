namespace TransactionManager.Api.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public DateOnly TransactionDate { get; set; }
    }
}
