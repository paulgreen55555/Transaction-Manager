namespace TransactionManager.Api.Dtos
{
    public class CreateTransactionDto
    {
        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}
