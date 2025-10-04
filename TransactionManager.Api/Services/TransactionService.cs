using System.Transactions;
using TransactionManager.Api.DTOs;
using TransactionManager.Api.Interfaces;

namespace TransactionManager.Api.Services
{
    public class TransactionService : ITransactionService
    {
        public void AddTransaction(TransactionDTO transaction)
        {
            throw new NotImplementedException();
        }

        public Transaction GetTransaction(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
