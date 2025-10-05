using System.ComponentModel.DataAnnotations;

namespace TransactionManager.Api.Dtos
{
    public record UpdateTransactionDto(
      [MaxLength(50)] string? Description,
      [Required][Range(0.01, int.MaxValue)] decimal Amount
  );
}
