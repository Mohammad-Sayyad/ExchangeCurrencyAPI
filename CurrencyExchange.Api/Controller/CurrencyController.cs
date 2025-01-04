using CurrencyExchange.Application.Interface;
using CurrencyExchange.Application.Service;
using CurrencyExchange.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyConverter _currencyConverter;
        private readonly CurrencyOnlineConverterService _currencyOnlineConverter;
        private readonly CurrencyEuroConverterService _currencyConverterServiceEuro;

        public CurrencyController(CurrencyConverter currencyConverter , CurrencyOnlineConverterService currencyOnlineConverter , CurrencyEuroConverterService currencyEuroConverterService)
        {
            _currencyConverter = currencyConverter;
            _currencyOnlineConverter = currencyOnlineConverter;
            _currencyConverterServiceEuro = currencyEuroConverterService;
        }

        [HttpGet("convert")]
        public IActionResult Convert(string fromCurrencyCode , string toCurrencyCode , decimal amount)
        {
            var fromCurrency = new Currency(fromCurrencyCode, fromCurrencyCode);
            var toCurrency = new Currency(toCurrencyCode, toCurrencyCode);

            var result = _currencyConverter.Convert(fromCurrency, toCurrency, amount);
            return Ok(new { AmountConverted = result });
        }

        [HttpGet("convert-to-irr")]
        public async Task<IActionResult> ConvertToIRR(decimal amount)
        {
            try
            {
                // فرض کنید می‌خواهیم دلار (USD) را به ریال ایران (IRR) تبدیل کنیم
                var result = await _currencyOnlineConverter.ConvertCurrency("USD", "IRR", amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("convert/euro-to-irr")]
        public async Task<IActionResult> ConvertFromEuroToIRR([FromQuery] decimal amount)
        {
            try
            {
                var result = await _currencyConverterServiceEuro.ConvertCurrency("EUR", "IRR", amount);
                return Ok(new { AmountInIRR = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
