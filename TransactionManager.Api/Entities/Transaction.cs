using System.ComponentModel.DataAnnotations;

namespace TransactionManager.Api.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

        public decimal Amount { get; set; }

        public DateOnly TransactionDate { get; set; }
    }
}
