using CurrencyExchange.Application.Interface;
using CurrencyExchange.Application.Service;
using CurrencyExchange.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient service
builder.Services.AddHttpClient(); // Registering HttpClient service globally

// Register services for different currency converters
builder.Services.AddScoped<CurrencyOnlineConverterService>();  // سرویس دلار
builder.Services.AddScoped<CurrencyEuroConverterService>();    // سرویس یورو

// Register ICurrencyConverterService with distinct names for each service
builder.Services.AddScoped<ICurrencyConverterService, CurrencyOnlineConverterService>();
builder.Services.AddScoped<ICurrencyConverterService, CurrencyEuroConverterService>();

// Register other services
builder.Services.AddScoped<IExchangeRateProvider, FixedExchangeRateProvider>();
builder.Services.AddScoped<CurrencyConverter>();

// Add services for controllers
builder.Services.AddControllers();

// Add endpoints API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Set up routing for controllers
app.MapControllers();

app.Run();
