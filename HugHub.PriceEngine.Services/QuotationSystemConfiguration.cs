namespace HugHub.PriceEngine.Services
{
    public class QuotationSystemConfiguration
    {
        public QuotationSystemConfiguration(string url)
        {
            Url = url;
        }
        
        public string Url { get; set; }
    }
}