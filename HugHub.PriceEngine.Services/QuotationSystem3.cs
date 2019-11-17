using System.Threading.Tasks;
using HugHub.PriceEngine.Models;

namespace HugHub.PriceEngine.Services
{
    public class QuotationSystem3: IQuotationSystem
    {
        private readonly QuotationSystemConfiguration _configuration;

        public QuotationSystem3(QuotationSystemConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ResponseResult<QuotationResult>> GetPrice(RiskData riskData)
        {
            var responseResult = new ResponseResult<QuotationResult>
            {
                Value = new QuotationResult {Price = 92.67M, Name = "zxcvbnm", Tax = 92.67M * 0.12M}
            };

            return Task.FromResult(responseResult);
        }
    }
}