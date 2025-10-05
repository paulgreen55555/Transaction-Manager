using TransactionManager.Api.Dtos;
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

            group.MapPost("/", async (ITransactionService transactionService, CreateTransactionDto createdTransaction) =>
            {
                var newTransaction = await transactionService.AddTransactionAsync(createdTransaction);

                return Results.CreatedAtRoute(GetTransactionEndpointName, new { id = newTransaction.Id }, newTransaction);
            })
            .WithParameterValidation();

            group.MapPut("/{id}", async (ITransactionService transactionService, Guid id, UpdateTransactionDto updatedTransaction) =>
            {
                await transactionService.UpdateTransactionAsync(id, updatedTransaction);

                return Results.NoContent();
            });

            group.MapDelete("/{id}", async (ITransactionService transactionService, Guid id) =>
            {
                await transactionService.DeleteTransactionAsync(id);

                return Results.NoContent();
            });

            return group;
        }
    }
}
