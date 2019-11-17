using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Models.Extensions;

namespace HugHub.PriceEngine.Services
{
    public class PriceRequestValidator<TValue>: IPriceRequestValidator<TValue>
    {
        public ResponseResult<TValue> Validate(PriceRequest request)
        {
            var result = new ResponseResult<TValue>()
                .Validate(
                    () => request.RiskData != null, nameof(request.RiskData), "Risk Data is missing");

            if (!result.Success) return result;

            return result
                .Validate(() => !string.IsNullOrEmpty(request.RiskData.FirstName), nameof(request.RiskData.FirstName),
                    "Firstname is required")
                .Validate(() => !string.IsNullOrEmpty(request.RiskData.LastName), nameof(request.RiskData.LastName),
                    "Lastname is required")
                .Validate(() =>
                        request.RiskData.Value.HasValue && request.RiskData.Value > 0,
                    nameof(request.RiskData.Value), "Value must be a positive number");
        }
    }
}