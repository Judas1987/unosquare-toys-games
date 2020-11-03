using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToysGames.Data.Models;
using Xunit;

namespace ToysGames.UnitTesting.Data
{
    public class ProductEntityUnitTest
    {
        
        [Fact]
        public void CreateNewProductExpectSuccess()
        {
            var product = new Product(new Guid(), "Very cool barby", "This is a really cool barby I found on internet",
                110, "Mattel", 1000);

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);
            
            Assert.False(actual, "Hello world");
        }

        [Fact]
        public void CreateNewProductIncorrectNameLengthExpectFailure()
        {
            
        }

        [Fact]
        public void CreateNewProductMissingNameExpectFailure()
        {
            
        }

        [Fact]
        public void CreateNewProductMissingDescriptionExpectSuccess()
        {
            
        }

        [Fact]
        public void CreateNewProductIncorrectDescriptionLengthExpectFailure()
        {
            
        }

        [Fact]
        public void CreateNewProductMissingAgeRestrictionExpectSuccess()
        {
            
        }

        [Fact]
        public void CreateNewProductIncorrectAgeRestrictionExpectFailure()
        {
            
        }

        [Fact]
        public void CreateNewProductMissingCompanyExpectFailure()
        {
            
        }

        [Fact]
        public void CreateNewProductIncorrectPriceExpectFailure()
        {
            
        }

        [Fact]
        public void CreateNewProductMissingPriceExpectFailure()
        {
            
        }
    }
}