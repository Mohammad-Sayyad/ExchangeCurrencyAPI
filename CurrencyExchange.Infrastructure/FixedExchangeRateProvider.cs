using CurrencyExchange.Application.Interface;
using CurrencyExchange.Domain;

namespace CurrencyExchange.Infrastructure;

public class FixedExchangeRateProvider : IExchangeRateProvider
{
    private readonly Dictionary<string, Dictionary<string, decimal>> _exchangeRates;

    public FixedExchangeRateProvider()
    {
        // مقداردهی اولیه نرخ تبدیل‌ها (برای مثال)
        _exchangeRates = new Dictionary<string, Dictionary<string, decimal>>()
        {
            { "USD", new Dictionary<string, decimal> { { "IRR", 82000 }, { "EUR", 0.91m } } },
            { "EUR", new Dictionary<string, decimal> { { "USD", 1.1m }, { "IRR", 90000 } } },
            // می‌تونید نرخ‌های دیگه رو هم اضافه کنید
        };
    }

    public decimal GetRate(Currency fromCurrency, Currency toCurrency)
    {
        // چک کردن null بودن ارزها
        if (fromCurrency == null || toCurrency == null)
        {
            throw new ArgumentNullException("پارامترهای ارز نمی‌توانند null باشند.");
        }

        // چک کردن وجود نرخ تبدیل در دیکشنری
        if (!_exchangeRates.ContainsKey(fromCurrency.Code) || !_exchangeRates[fromCurrency.Code].ContainsKey(toCurrency.Code))
        {
            throw new InvalidOperationException($"نرخ تبدیل از {fromCurrency.Code} به {toCurrency.Code} موجود نیست.");
        }

    
        return _exchangeRates[fromCurrency.Code][toCurrency.Code];
    }

}
