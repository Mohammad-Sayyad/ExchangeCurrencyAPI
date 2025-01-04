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
    public class CurrencyOnlineConverterService : ICurrencyConverterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://api.exchangerate-api.com/v4/latest/USD"; 
       // private readonly string _apiUrlEuoro = "http://api.currencylayer.com/live?access_key=YOUR_ACCESS_KEY&currencies=IRR";


        public CurrencyOnlineConverterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            // دریافت پاسخ از API
            var response = await _httpClient.GetAsync(_apiUrl);

            // بررسی وضعیت موفقیت‌آمیز بودن درخواست
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch exchange rate data.");
            }

            // خواندن محتوای پاسخ به صورت رشته
            var responseContent = await response.Content.ReadAsStringAsync();

            // تبدیل رشته JSON به شیء ExchangeRateApiResponse
            var exchangeRateData = JsonConvert.DeserializeObject<ExchangeRateApiResponse>(responseContent);

            // بررسی نرخ ارز و محاسبه مقدار تبدیل شده
            if (exchangeRateData?.Rates.ContainsKey(toCurrency) == true)
            {
                var rate = exchangeRateData.Rates[toCurrency];
                return amount * rate;
            }

            throw new Exception("Invalid currency or rate not found.");
        }
    }

}
