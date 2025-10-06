namespace TransactionManager.Api.Dtos
{
    public record ConvertedTransactionDto(
        Guid id,
        string? Description,
        DateOnly TransactionDate,
        decimal Amount,
        string? CurrencyCode,
        decimal Rate,
        decimal ConvertedAmount
    );
}
