using CurrencyExchange.Application.Interface;
using CurrencyExchange.Application.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Service
{
    public class CustomCurrencyService : ICurrencyConverterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrlTemplate = "https://api.exchangerate-api.com/v4/latest/{0}";


        public CustomCurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            var apiUrl = string.Format(_apiUrlTemplate, fromCurrency);
            var response = await _httpClient.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Faild to fetch exchange rate data");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var exchangeRateData = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responseContent);

            if(exchangeRateData?.Rates.ContainsKey(toCurrency) == true)
            {
                var rate = exchangeRateData.Rates[toCurrency];
                return amount * rate;
            }

            throw new NotImplementedException("invalid currency or rate not found");
        }
    }
}
