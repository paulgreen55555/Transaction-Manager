using TransactionManager.Api.Converters;
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
                Amount = CurrencyConverter.RoundToCent(transaction.Amount),
                TransactionDate = transaction.TransactionDate,
            };
        }

        public static Transaction ToEntity(this CreateTransactionDto transaction)
        {
            return new Transaction()
            {
                Description = transaction.Description,
                Amount = CurrencyConverter.RoundToCent(transaction.Amount),
            };
        }

        public static TransactionDto ToDto(this Transaction transaction)
        {
            return new TransactionDto(transaction.Id,
                transaction.Description,
                transaction.Amount,
                transaction.TransactionDate
             );
        }
    }
}
