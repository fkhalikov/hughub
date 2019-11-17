using HugHub.PriceEngine.Models.Extensions;
using Xunit;

namespace HugHub.PriceEngine.Models.Tests
{
    public class OperationResultExtensionsTests
    {
        [Fact]
        public void ItCreatesPriceResult()
        {
            var quotationResult = new QuotationResult
            {
                Name = "Insurance company",
                Tax = 5,
                Price = 2
            };

            var priceResult = quotationResult.ToPriceResult();

            Assert.NotNull(priceResult);
            Assert.Equal(quotationResult.Name, priceResult.InsurerName);
            Assert.Equal(quotationResult.Price, priceResult.Price);
            Assert.Equal(quotationResult.Tax, priceResult.Tax);
        }
    }
}