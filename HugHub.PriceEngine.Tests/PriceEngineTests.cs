using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Services;
using Moq;
using Xunit;

namespace HugHub.PriceEngine.Tests
{
    public class PriceEngineTests
    {
        [Fact]
        public async Task ItReturnsLowestQuotePrice()
        {
            var quotationSystems = new List<IQuotationSystem>
            {
                CreateQuotationSystemMock(3).Object,
                CreateQuotationSystemMock(2).Object,
                CreateQuotationSystemMock(4).Object
            };

            var priceEngine = new PriceEngine(CreatePriceRequestValidatorMock().Object, quotationSystems);
            var priceResult = await priceEngine.GetPrice(new PriceRequest());

            Assert.True(priceResult.Success);
            Assert.Equal(2, priceResult.Value.Price);
        }
        
        
        [Fact]
        public async Task ItFailsIfNoQuotesReturned()
        {
            var priceEngine = new PriceEngine(CreatePriceRequestValidatorMock().Object, new List<IQuotationSystem>());
            var priceResult = await priceEngine.GetPrice(new PriceRequest());

            Assert.False(priceResult.Success);
            Assert.Equal("QUOTATION", priceResult.Error.First().Key);
            Assert.Equal("Failed to fetch a quote", priceResult.Error.First().Value.First());
        }

        private static Mock<IQuotationSystem> CreateQuotationSystemMock(decimal price)
        {
            var quotationSystemMock = new Mock<IQuotationSystem>();
            quotationSystemMock.Setup(x => x.GetPrice(It.IsAny<RiskData>()))
                .ReturnsAsync(() => new ResponseResult<QuotationResult>()
                {
                    Value = new QuotationResult {Price = price}
                });

            return quotationSystemMock;
        }
        
        private static Mock<IPriceRequestValidator<PriceResult>> CreatePriceRequestValidatorMock()
        {
            var priceRequestValidatorMock = new Mock<IPriceRequestValidator<PriceResult>>();
            priceRequestValidatorMock.Setup(x => x.Validate(It.IsAny<PriceRequest>()))
                .Returns(() => new ResponseResult<PriceResult>());

            return priceRequestValidatorMock;
        }
    }
}