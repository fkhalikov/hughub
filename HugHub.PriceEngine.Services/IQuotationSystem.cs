using System.Threading.Tasks;
using HugHub.PriceEngine.Models;

namespace HugHub.PriceEngine.Services
{
    public interface IQuotationSystem
    {
        Task<ResponseResult<QuotationResult>> GetPrice(RiskData riskData);
    }
}