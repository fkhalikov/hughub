using System.Linq;
using HugHub.PriceEngine.Models;
using Xunit;

namespace HugHub.PriceEngine.Services.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(0.1)]
        [InlineData(10)]
        public void ItSucceeds(double? value)
        {
            var validator = new PriceRequestValidator<object>();
            var result = validator.Validate(CreatePriceRequest("FirstName","LastName", (decimal?)value));
            
            Assert.True(result.Success);
            Assert.Empty(result.Error);
        }      
        
        [Fact]
        public void ItFailsIfRiskDataNotSupplied()
        {
            var validator = new PriceRequestValidator<object>();
            var result = validator.Validate(new PriceRequest());
            
            Assert.False(result.Success);
            Assert.Single(result.Error);
            Assert.Equal("RiskData", result.Error.First().Key);
            Assert.Equal("Risk Data is missing", result.Error.First().Value.First());
        }        
        
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ItFailsIfFirstnameNotSupplied(string firstName)
        {
            var validator = new PriceRequestValidator<object>();
            var result = validator.Validate(CreatePriceRequest(firstName, "LastName", 10));
            
            Assert.False(result.Success);
            Assert.Single(result.Error);
            Assert.Equal("FirstName", result.Error.First().Key);
            Assert.Equal("Firstname is required", result.Error.First().Value.First());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ItFailsIfLastnameNotSupplied(string lastName)
        {
            var validator = new PriceRequestValidator<object>();
            var result = validator.Validate(CreatePriceRequest("FirstName", lastName, 10));

            Assert.False(result.Success);
            Assert.Single(result.Error);
            Assert.Equal("LastName", result.Error.First().Key);
            Assert.Equal("Lastname is required", result.Error.First().Value.First());
        }

        [Theory]
        [InlineData(-1.0)]
        [InlineData(0.0)]
        [InlineData(null)]
        public void ItFailsIfValueIsInvalid(double? value)
        {
            var validator = new PriceRequestValidator<object>();
            var result = validator.Validate(CreatePriceRequest("FirstName", "LastName", (decimal?)value));

            Assert.False(result.Success);
            Assert.Single(result.Error);
            Assert.Equal("Value", result.Error.First().Key);
            Assert.Equal("Value must be a positive number", result.Error.First().Value.First());
        }

        private static PriceRequest CreatePriceRequest(string firstName, string lastName, decimal? value)
        {
            return new PriceRequest
            {
                RiskData = new RiskData
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Value = value
                }
            };
        }
    }
}