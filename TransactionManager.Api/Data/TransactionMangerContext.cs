using Microsoft.EntityFrameworkCore;
using TransactionManager.Api.Entities;

namespace TransactionManager.Api.Data
{
    public class TransactionMangerContext(DbContextOptions<TransactionMangerContext> options)
        : DbContext(options) 
    {
        public DbSet<Transaction> Transactions => Set<Transaction>();
    }
}
