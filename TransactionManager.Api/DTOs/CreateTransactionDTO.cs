namespace TransactionManager.Api.DTOs
{
    public class CreateTransactionDTO
    {
        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}
