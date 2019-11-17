using HugHub.PriceEngine.Models;

namespace HugHub.PriceEngine.Services
{
    public interface IPriceRequestValidator<TValue>
    {
        ResponseResult<TValue> Validate(PriceRequest request);
    }
}