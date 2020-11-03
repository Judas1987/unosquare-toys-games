using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToysGames.Data.Models;
using Xunit;

namespace ToysGames.UnitTesting.Data
{
    public class ProductEntityUnitTest
    {
        /// <summary>
        /// This test case tries to create or instantiate a new product with all the required information.
        /// </summary>
        [Fact]
        public void CreateNewProductExpectSuccess()
        {
            var product = new Product(new Guid(), "Very cool barby", "This is a really cool barby I found on internet",
                5, "Mattel", 1000);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.True(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 0, "The validation results are not equal to zero.");
        }

        /// <summary>
        /// This test cases tries to create or instantiate a new product with greater length than allowed.
        /// </summary>
        [Fact]
        public void CreateNewProductIncorrectNameLengthExpectFailure()
        {
            var product = new Product(new Guid(), "Very cool barby with more characters that are actually allowed",
                "This is a really cool barby I found on internet",
                5, "Mattel", 1000);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product name must be of 50 characters at maximum.", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This test case tries to create or instantiate a new product with missing name.
        /// </summary>
        [Fact]
        public void CreateNewProductMissingNameExpectFailure()
        {
            var product = new Product(new Guid(), null,
                "This is a really cool barby I found on internet",
                5, "Mattel", 1000);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product name is mandatory.", validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This test case tries to create or instantiate a new product with missing description.
        /// </summary>
        [Fact]
        public void CreateNewProductMissingDescriptionExpectSuccess()
        {
            var product = new Product(new Guid(), "Cool car", null, 10, "Mattel", 243);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.True(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 0, "The validation results are not equal to one.");
        }

        /// <summary>
        /// This test case tries to create a new product with incorrect description length.
        /// </summary>
        [Fact]
        public void CreateNewProductIncorrectDescriptionLengthExpectFailure()
        {
            var product = new Product(new Guid(), "Cool car",
                "This is a very cool car that I found on internet, I really like how it looks at night when I play with it. I think all cars should be like this because most of the time I am busy during the day.",
                10, "Mattel", 243);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product description must be of 100 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This test case tries to create or instantiate a new product with missing age restriction.
        /// </summary>
        [Fact]
        public void CreateNewProductMissingAgeRestrictionExpectSuccess()
        {
            var product = new Product(new Guid(), "Cool car",
                "This is a very cool car that I found on internet",
                null, "Mattel", 243);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.True(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 0, "The validation results are not equal to one.");
        }

        /// <summary>
        /// This test case tries to create or instantiate a new product with incorrect age restriction.
        /// </summary>
        [Fact]
        public void CreateNewProductIncorrectAgeRestrictionExpectFailure()
        {
            var product = new Product(new Guid(), "Cool car",
                "This is a very cool car that I found on internet",
                101, "Mattel", 243);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("Value for AgeRestriction must be between 0 and 100",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This test case tries to create or instantiate a new product with missing company.
        /// </summary>
        [Fact]
        public void CreateNewProductMissingCompanyExpectFailure()
        {
            var product = new Product(new Guid(), "Cool car",
                "This is a very cool car that I found on internet",
                10, null, 243);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product company is mandatory.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This test case tries to create or instantiate a new product with incorrect price.
        /// </summary>
        [Fact]
        public void CreateNewProductIncorrectPriceExpectFailure()
        {
            var product = new Product(new Guid(), "Cool car",
                "This is a very cool car that I found on internet",
                10, "This is a company with a really long name so, it will make our system to fail.", 243);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product company must be of 50 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tries to create or instantiate a new product with missing price.
        /// </summary>
        [Fact]
        public void CreateNewProductMissingPriceExpectFailure()
        {
            var product = new Product(new Guid(), "Cool car",
                "This is a very cool car that I found on internet",
                10, "Mattel", -12.36);

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(product, new ValidationContext(product), validationResults, true);

            Assert.False(isValid, "The product model did not pass the entity validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("Price must be between $1 and $1000",
                validationResults[0].ErrorMessage);
        }
    }
}