using System.Collections.Generic;
using System.Threading.Tasks;
using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Models.Extensions;
using HugHub.PriceEngine.Services;

namespace HugHub.PriceEngine
{
    public class PriceEngine
    {
        private readonly IPriceRequestValidator<PriceResult> _priceRequestValidator;
        private readonly IEnumerable<IQuotationSystem> _quotationSystems;

        public PriceEngine(
            IPriceRequestValidator<PriceResult> priceRequestValidator,
            IEnumerable<IQuotationSystem> quotationSystems)
        {
            _priceRequestValidator = priceRequestValidator;
            _quotationSystems = quotationSystems;
        }
        
        public async Task<ResponseResult<PriceResult>> GetPrice(PriceRequest request)
        {
            var result = _priceRequestValidator.Validate(request);

            if (!result.Success) return result;

            foreach(var quotationSystem in _quotationSystems) {
                var quotationResult = await quotationSystem.GetPrice(request.RiskData);

                if (!quotationResult.Success) continue;
                
                if (result.Value == null)
                {
                    result.Value = quotationResult.Value.ToPriceResult();
                }
                else
                {
                    if (result.Value.Price > quotationResult.Value.Price)
                    {
                        result.Value = quotationResult.Value.ToPriceResult();
                    }
                }
            }
            
            if (!result.HasValue)
            {
                result.AddError(
                    PriceEngineErrorCodes.QuotationError.Code,
                    PriceEngineErrorCodes.QuotationError.Error);
            }

            return result;
        }
    }
}