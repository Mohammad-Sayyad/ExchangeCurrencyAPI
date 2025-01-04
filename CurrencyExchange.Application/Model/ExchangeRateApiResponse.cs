using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Application.Model
{
    public class ExchangeRateApiResponse
    {
        // نرخ ارزها که در آن نرخ‌های تبدیل برای هر ارز قرار دارد
        public Dictionary<string, decimal> Rates { get; set; }

        // ارز پایه که نرخ‌ها برای آن محاسبه شده است (مثلاً USD)
        public string Base { get; set; }

        // تاریخ آخرین به‌روزرسانی نرخ‌ها
        public DateTime Date { get; set; }
    }
}
