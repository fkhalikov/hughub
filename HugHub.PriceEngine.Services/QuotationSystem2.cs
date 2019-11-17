using System.Threading.Tasks;
using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Models.Extensions;

namespace HugHub.PriceEngine.Services
{
    public class QuotationSystem2: IQuotationSystem
    {
        private readonly QuotationSystemConfiguration _configuration;

        public QuotationSystem2(QuotationSystemConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ResponseResult<QuotationResult>> GetPrice(RiskData riskData)
        {
            var responseResult = new ResponseResult<QuotationResult>();

            if (!(riskData.Make == "examplemake1" ||
                  riskData.Make == "examplemake2" ||
                  riskData.Make == "examplemake3"))
            {
                responseResult.AddError(nameof(riskData.Make), $"Make '{riskData.Make}' is not supported.");
                return Task.FromResult(responseResult);
            }

            responseResult.Value = new QuotationResult
            {
                Price = 234.56M,
                Name = "qewtrywrh",
                Tax = 234.56M * 0.12M
            };

            return Task.FromResult(responseResult);
        }
    }
}