using System;
using System.Threading.Tasks;
using HugHub.PriceEngine.Models;
using HugHub.PriceEngine.Models.Extensions;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DateTime? DOB = null;
            decimal? value = null;

            if (DateTime.TryParse(args[4], out var parsedDOB)) DOB = parsedDOB;
            if (decimal.TryParse(args[3], out var parsedValue)) value = parsedValue;
            
            var request = new PriceRequest
            {
                RiskData = new RiskData //hardcoded here, but would normally be from user input above
                {
                    DOB = DOB,
                    FirstName = args[0],
                    LastName = args[1],
                    Make = args[2],
                    Value = value 
                }
            };

            var priceEngine = DIContainer.ResolvePriceEngine();
            var price = await priceEngine.GetPrice(request);

            Console.WriteLine(!price.Success
                ? $"There was an error - {price.SquashErrors()}"
                : $"You price is {price.Value.Price}, from insurer: {price.Value.InsurerName}. This includes tax of {price.Value.Tax}");

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
