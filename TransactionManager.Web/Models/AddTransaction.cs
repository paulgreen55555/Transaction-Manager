using System.ComponentModel.DataAnnotations;

namespace TransactionManager.Web.Models
{
    public class AddTransaction
    {
        [Required]
        [MaxLength(50)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Amount should be more than 0.01")]
        [Range(0.01, int.MaxValue)]
        public decimal Amount { get; set; }
    }
}
