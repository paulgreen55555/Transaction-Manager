namespace TransactionManager.Api.Dtos
{
    public record TransactionDto(
        Guid Id,
        string? Description,
        decimal Amount,
        DateOnly TransactionDate
    );
}
