using Microsoft.EntityFrameworkCore;
using TransactionManager.Api.Data;
using TransactionManager.Api.Dtos;
using TransactionManager.Api.Entities;
using TransactionManager.Api.Services;

namespace TransactionManager.Tests
{
    public class TransactionServiceTests
    {
        private readonly CurrencyRateService _currencyRateService;

        private TransactionMangerContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TransactionMangerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new TransactionMangerContext(options);
        }

        [Fact]
        public async void AddTransactionAsync_AddsTransaction()
        {
            using var context = CreateDbContext();
            var service = new TransactionService(context, _currencyRateService);
            var dto = new CreateTransactionDto("Transaction1", 9.50m);

            var result = await service.AddTransactionAsync(dto);

            Assert.Single(context.Transactions);
            Assert.Equal("Transaction1", result.Description);
            Assert.Equal(9.50m, result.Amount);
        }

        [Fact]
        public async Task GetTransactionsAsync_ReturnsListOfTransactions()
        {
            using var context = CreateDbContext();
            List<Transaction> transactions = new()
            {
                new Transaction(){
                    Id = Guid.NewGuid(),
                    Description = "Transaction1",
                    Amount = 10.99m,
                    TransactionDate = DateOnly.FromDateTime(DateTime.Now)
                },
                new Transaction(){
                    Id = Guid.NewGuid(),
                    Description = "Transaction2",
                    Amount = 10.99m,
                    TransactionDate = DateOnly.FromDateTime(DateTime.Now)
                },
            };

            context.Transactions.AddRange(transactions);
            await context.SaveChangesAsync();

            var service = new TransactionService(context, _currencyRateService);
            var result = await service.GetTransactionsAsync();

            var resultList = result.ToList();

            Assert.NotNull(result);
            Assert.Equal("Transaction1", resultList[0].Description);
            Assert.Equal("Transaction2", resultList[1].Description);
        }

        [Fact]
        public async Task GetTransactionAsync_ReturnsTransaction()
        {
            using var context = CreateDbContext();
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Transaction1",
                Amount = 10.99m,
                TransactionDate = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            var service = new TransactionService(context, _currencyRateService);
            var result = await service.GetTransactionAsync(transaction.Id);

            Assert.NotNull(result);
            Assert.Equal("Transaction1", result.Description);
        }

        [Fact]
        public async Task UpdateTransactionsAsync_UpdatesTransaction()
        {
            using var context = CreateDbContext();
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Transaction1",
                Amount = 10.99m,
                TransactionDate = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            var dto = new UpdateTransactionDto("Transaction3", 109.50m);

            var service = new TransactionService(context, _currencyRateService);
            var result = await service.UpdateTransactionAsync(transaction.Id, dto);

            Assert.NotNull(result);
            Assert.Equal("Transaction3", result.Description);
            Assert.Equal(109.50m, result.Amount);
        }

        [Fact]
        public async Task DeleteTransactionsAsync_DeletesTransaction()
        {
            using var context = CreateDbContext();
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Description = "Transaction1",
                Amount = 10.99m,
                TransactionDate = DateOnly.FromDateTime(DateTime.Now)
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            var service = new TransactionService(context, _currencyRateService);
            await service.DeleteTransactionAsync(transaction.Id);

            var deleted = await context.Transactions.FindAsync(transaction.Id);
            Assert.Null(deleted);
        }
    }
}