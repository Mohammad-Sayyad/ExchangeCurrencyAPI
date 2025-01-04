using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Interface
{
    public interface ICurrencyConverterService
    {
        Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount);
    }
}
