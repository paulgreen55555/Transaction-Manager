using TransactionManager.Api.DTOs;
using TransactionManager.Api.Entities;

namespace TransactionManager.Api.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();

        Task<Transaction?> GetTransactionAsync(Guid id);

        Task<Transaction> AddTransactionAsync(TransactionDTO transaction);
    }
}
