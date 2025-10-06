﻿using TransactionManager.Api.Dtos;

namespace TransactionManager.Api.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetTransactionsAsync();

        Task<TransactionDto?> GetTransactionAsync(Guid id);

        Task<TransactionDto> AddTransactionAsync(CreateTransactionDto transactionDto);

        Task<TransactionDto?> UpdateTransactionAsync(Guid id, UpdateTransactionDto transactionDto);

        Task DeleteTransactionAsync(Guid id);

        Task<ConvertedTransactionDto?> GetConvertedTransactionAsync(Guid id, string currencyCode);
    }
}
