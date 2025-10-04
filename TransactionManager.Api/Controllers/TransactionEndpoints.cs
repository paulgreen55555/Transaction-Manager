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

            group.MapGet("/", async (ITransactionService transactionService) =>
            {
                var result = await transactionService.GetTransactionsAsync();

                return Results.Ok(result);
            });

            group.MapGet("/{id}", async (ITransactionService transactionService, Guid id) =>
            {
                var result = await transactionService.GetTransactionAsync(id);
                                
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName(GetTransactionEndpointName);

            group.MapPost("/", async (ITransactionService transactionService, CreateTransactionDTO createdTransaction) =>
            {
                var transactionDTO = new TransactionDTO()
                {
                    Amount = createdTransaction.Amount,
                    Description = createdTransaction.Description,
                    TransactionDate = DateTime.Now,
                };

                var newTransaction = await transactionService.AddTransactionAsync(transactionDTO);

                return Results.CreatedAtRoute(GetTransactionEndpointName, new { id = newTransaction.Id }, newTransaction);
            });

            return group;
        }
    }
}
