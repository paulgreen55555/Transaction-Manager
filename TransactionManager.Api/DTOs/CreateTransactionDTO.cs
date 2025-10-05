using System.ComponentModel.DataAnnotations;

namespace TransactionManager.Api.Dtos
{
    public record CreateTransactionDto(
        [MaxLength(50)] string? Description,
        [Required][Range(0.01, int.MaxValue)] decimal Amount
    );
}
