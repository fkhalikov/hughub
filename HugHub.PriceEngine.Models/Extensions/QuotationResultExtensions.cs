namespace HugHub.PriceEngine.Models.Extensions
{
    public static class QuotationResultExtensions
    {
        public static PriceResult ToPriceResult(this QuotationResult quotationResult)
        {
            return new PriceResult
            {
                InsurerName = quotationResult.Name,
                Price = quotationResult.Price,
                Tax = quotationResult.Tax
            };
        }
    }
}