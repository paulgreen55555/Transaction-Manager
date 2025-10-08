using TransactionManager.Api.Controllers;
using TransactionManager.Api.Data;
using TransactionManager.Api.Interfaces;
using TransactionManager.Api.Middleware;
using TransactionManager.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ICurrencyRateService, CurrencyRateService>();

builder.Services.AddScoped<ITransactionService, TransactionService>();

var connectionString = builder.Configuration.GetConnectionString("TransactionManger");
builder.Services.AddSqlite<TransactionMangerContext>(connectionString);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTransactionEndpoints();
app.MapCurrencyEndpoints();

app.MigrateDb();

app.Run();
