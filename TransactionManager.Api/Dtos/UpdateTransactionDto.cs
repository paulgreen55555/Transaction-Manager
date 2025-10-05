namespace TransactionManager.Api.Dtos
{
    public class UpdateTransactionDto
    {
        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}
