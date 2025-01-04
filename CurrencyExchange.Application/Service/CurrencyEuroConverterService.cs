using CurrencyExchange.Application.Interface;
using CurrencyExchange.Application.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Service
{
    public class CurrencyEuroConverterService : ICurrencyConverterService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrlEuro = "https://api.exchangerate-api.com/v4/latest/EUR"; // URL برای یورو

        public CurrencyEuroConverterService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient)); // اطمینان از مقداردهی HttpClient
        }

        public async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            // دریافت پاسخ از API
            var response = await _httpClient.GetAsync(_apiUrlEuro);

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
