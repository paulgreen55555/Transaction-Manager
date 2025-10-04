using TransactionManager.Api.DTOs;
using TransactionManager.Api.Interfaces;

namespace TransactionManager.Api.Controllers
{
    public static class TransactionEndpoints
    {
        public static RouteGroupBuilder MapTransactionEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/transactions");

            group.MapGet("/", (ITransactionService transactionService) =>
            {
                var result = transactionService.GetTransactions();

                return Results.Ok(result);
            });

            group.MapGet("/{id}", (ITransactionService transactionService, Guid id) =>
            {
                var result = transactionService.GetTransaction(id);

                return Results.Ok(result);
            });

            group.MapPost("/", (ITransactionService transactionService, CreateTransactionDTO newTransaction) =>
            {
                var transaction = new TransactionDTO();

                transactionService.AddTransaction(transaction);
            });

            return group;
        }
    }
}
