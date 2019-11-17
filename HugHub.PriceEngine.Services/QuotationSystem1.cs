using System.Threading.Tasks;
using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Models.Extensions;

namespace HugHub.PriceEngine.Services
{
    public class QuotationSystem1: IQuotationSystem
    {
        private readonly QuotationSystemConfiguration _quotationSystemConfiguration;

        public QuotationSystem1(QuotationSystemConfiguration quotationSystemConfiguration)
        {
            _quotationSystemConfiguration = quotationSystemConfiguration;
        }

        public Task<ResponseResult<QuotationResult>> GetPrice(RiskData riskData)
        {
            var responseResult = new ResponseResult<QuotationResult>();

            if (riskData.DOB == null)
            {
                responseResult.AddError(nameof(riskData.DOB), "DOB is mandatory.");
                return Task.FromResult(responseResult);
            }

            responseResult.Value = new QuotationResult
            {
                Price = 123.45M,
                Name = "Test Name",
                Tax = 123.45M * 0.12M
            };

            return Task.FromResult(responseResult);
        }
    }
}