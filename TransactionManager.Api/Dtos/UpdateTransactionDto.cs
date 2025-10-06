using System.ComponentModel.DataAnnotations;

namespace TransactionManager.Api.Dtos
{
    public record UpdateTransactionDto(
      [MaxLength(50)] string? Description,
      [Range(0.01, int.MaxValue)] decimal? Amount
  );
}
