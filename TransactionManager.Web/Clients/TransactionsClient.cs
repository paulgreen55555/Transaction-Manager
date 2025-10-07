using TransactionManager.Web.Models;

namespace TransactionManager.Web.Clients
{
    public class TransactionsClient(HttpClient httpClient)
    {
        public async Task<Transaction[]> GetTransactionsAsync() =>
            await httpClient.GetFromJsonAsync<Transaction[]>("transactions") ?? [];

        public async Task AddTransactionAsync(AddTransaction transaction) =>
            await httpClient.PostAsJsonAsync("transactions", transaction);

         public async Task<Transaction> GetTransactionAsync(Guid id) =>
            await httpClient.GetFromJsonAsync<Transaction>($"transactions/{id}") ??
            throw new Exception("Could not find transaction");

        public async Task UpdateTransactionAsync(AddTransaction updatedTransaction, Guid id) =>
            await httpClient.PutAsJsonAsync($"transactions/{id}", updatedTransaction);

        public async Task DeteteTransactionAsync(Guid id) =>
            await httpClient.DeleteAsync($"transactions/{id}");
    }
}
