using System.Collections.Generic;
using HugHub.PriceEngine;
using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Services;

namespace ConsoleApp1
{
    public static class DIContainer
    {
        public static PriceEngine ResolvePriceEngine()
        {
            var quotationSystems = new List<IQuotationSystem>
            {
                new QuotationSystem1(new QuotationSystemConfiguration("http://quote-system-1.com:1234")),
                new QuotationSystem2(new QuotationSystemConfiguration("http://quote-system-2.com:1235")),
                new QuotationSystem3(new QuotationSystemConfiguration("http://quote-system-3.com:100"))
            };
            
            return new PriceEngine(new PriceRequestValidator<PriceResult>(), quotationSystems);
        }
    }
}