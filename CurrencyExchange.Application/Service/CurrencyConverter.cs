using CurrencyExchange.Application.Interface;
using CurrencyExchange.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Service
{
    public class CurrencyConverter
    {
        private readonly IExchangeRateProvider _exchangeRateProvider;

        public CurrencyConverter(IExchangeRateProvider exchangeRateProvider)
        {
            _exchangeRateProvider = exchangeRateProvider;
        }

        public decimal Convert(Currency fromCurrency , Currency toCurrency , decimal amount)
        {
            var rate = _exchangeRateProvider.GetRate(fromCurrency, toCurrency);
            return amount * rate;
        }
    }
}
