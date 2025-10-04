using TransactionManager.Api.DTOs;
using TransactionManager.Api.Interfaces;

namespace TransactionManager.Api.Controllers
{
    public static class TransactionEndpoints
    {
        public static RouteGroupBuilder MapTransactionEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/transactions");

            const string GetTransactionEndpointName = "GetTransaction";

            group.MapGet("/", (ITransactionService transactionService) =>
            {
                var result = transactionService.GetTransactions();

                return Results.Ok(result);
            });

            group.MapGet("/{id}", (ITransactionService transactionService, Guid id) =>
            {
                var result = transactionService.GetTransaction(id);
                                
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName(GetTransactionEndpointName);

            group.MapPost("/", (ITransactionService transactionService, CreateTransactionDTO createdTransaction) =>
            {
                var transactionDTO = new TransactionDTO()
                {
                    Amount = createdTransaction.Amount,
                    Description = createdTransaction.Description,
                    TransactionDate = DateTime.Now,
                };

                var newTransaction = transactionService.AddTransaction(transactionDTO);

                return Results.CreatedAtRoute(GetTransactionEndpointName, new { id = newTransaction.Id }, newTransaction);
            });

            return group;
        }
    }
}
