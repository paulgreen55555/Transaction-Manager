using Microsoft.EntityFrameworkCore;
using TransactionManager.Api.Data;
using TransactionManager.Api.Dtos;
using TransactionManager.Api.Entities;
using TransactionManager.Api.Interfaces;
using TransactionManager.Api.Mapping;

namespace TransactionManager.Api.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly TransactionMangerContext _dbContext;

        public TransactionService(TransactionMangerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TransactionDto> AddTransactionAsync(CreateTransactionDto transaction)
        {
            var newTransaction = new Transaction()
            {
                Id = Guid.NewGuid(),
                Description = transaction.Description,
                Amount = transaction.Amount,
                TransactionDate = DateTime.Now,
            };

            await _dbContext.Transactions.AddAsync(newTransaction);
            await _dbContext.SaveChangesAsync();

            return newTransaction.ToDto();
        }

        public async Task<TransactionDto?> GetTransactionAsync(Guid id)
        {
            var transaction = await _dbContext.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction is null)
            {
                throw new KeyNotFoundException($"Transaction with Id {id} not found");
            }

            return transaction.ToDto();
        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsAsync()
        {
            var transactions = await _dbContext.Transactions
                .AsNoTracking()
                .ToListAsync();

            return transactions.Select(t => t.ToDto());
        }

        public async Task<TransactionDto?> UpdateTransactionAsync(Guid id, UpdateTransactionDto transactionDto)
        {
            var existingTransaction = await _dbContext.Transactions.FindAsync(id);

            if (existingTransaction == null)
            {
                throw new KeyNotFoundException($"Transaction with Id {id} not found");
            }

            existingTransaction.Description = transactionDto.Description;
            existingTransaction.Amount = transactionDto.Amount;
            existingTransaction.TransactionDate = DateTime.Now;

            _dbContext.Transactions.Update(existingTransaction);
            await _dbContext.SaveChangesAsync();

            return existingTransaction?.ToDto();
        }

        public async Task DeleteTransactionAsync(Guid id)
        {
            var existingTransaction = await _dbContext.Transactions.FindAsync(id);

            if (existingTransaction == null)
            {
                throw new KeyNotFoundException($"Transaction with Id {id} not found");
            }

            _dbContext.Transactions.Remove(existingTransaction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
