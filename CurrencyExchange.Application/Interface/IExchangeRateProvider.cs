using CurrencyExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interface
{
    public interface IExchangeRateProvider
    {
        decimal GetRate(Currency fromCurrency, Currency toCurrency);
    }
}
