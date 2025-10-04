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

        public async Task<Transaction> AddTransactionAsync(TransactionDTO transaction)
        {
            var newTransaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Description = transaction.Description,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
            };

            await _dbContext.Transactions.AddAsync(newTransaction);
            await _dbContext.SaveChangesAsync();

            return newTransaction;
        }

        public async Task<Transaction?> GetTransactionAsync(Guid id)
        {
            return await _dbContext.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
        {
            return await _dbContext.Transactions
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
