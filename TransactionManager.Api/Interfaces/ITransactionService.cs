using System.Transactions;
using TransactionManager.Api.DTOs;

namespace TransactionManager.Api.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions();

        Transaction GetTransaction(Guid id);

        void AddTransaction(TransactionDTO transaction);
    }
}
