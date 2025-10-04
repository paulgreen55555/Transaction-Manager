using Microsoft.EntityFrameworkCore;
using TransactionManager.Api.Data;
using TransactionManager.Api.DTOs;
using TransactionManager.Api.Entities;
using TransactionManager.Api.Interfaces;

namespace TransactionManager.Api.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly TransactionMangerContext _dbContext;

        public TransactionService(TransactionMangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Transaction AddTransaction(TransactionDTO transaction)
        {
            var newTransaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Description = transaction.Description,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
            };

            _dbContext.Transactions.Add(newTransaction);
            _dbContext.SaveChanges();

            return newTransaction;
        }

        public Transaction? GetTransaction(Guid id)
        {
            return _dbContext.Transactions
                .AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _dbContext.Transactions
                .AsNoTracking()
                .ToList();
        }
    }
}
