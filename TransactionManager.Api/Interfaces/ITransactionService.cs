using TransactionManager.Api.DTOs;
using TransactionManager.Api.Entities;

namespace TransactionManager.Api.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions();

        Transaction? GetTransaction(Guid id);

        Transaction AddTransaction(TransactionDTO transaction);
    }
}
