using TransactionManager.Api.Dtos;
using TransactionManager.Api.Entities;

namespace TransactionManager.Api.Mapping
{
    public static class TransactionMapping
    {
        public static Transaction ToEntity(this TransactionDto transaction)
        {
            return new Transaction()
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
            };
        }

        public static TransactionDto ToDto(this Transaction transaction)
        {
            return new TransactionDto()
            {
                Id = transaction.Id,
                Description = transaction.Description,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
            };
        }
    }
}
