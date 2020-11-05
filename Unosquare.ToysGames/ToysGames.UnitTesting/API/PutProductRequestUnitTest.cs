using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToysGames.API.Models;
using Xunit;

namespace ToysGames.UnitTesting.API
{
    public class PutProductRequestUnitTest
    {
        
        /// <summary>
        /// This method creates a new instance of the post product request class.
        /// </summary>
        [Fact]
        public void CreateNewInstanceExpectSuccess()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);
        }

        /// <summary>
        /// This method tests that the data assigned in the class does not get transformed inside of the object.
        /// </summary>
        [Fact]
        public void CreateInstanceAndAssignDataExpectSuccess()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);

            putProductRequest.Name = "Name";
            putProductRequest.Description = "Description";
            putProductRequest.Company = "Company";
            putProductRequest.Price = 1;
            putProductRequest.AgeRestriction = 1;

            Assert.Equal("Name", putProductRequest.Name);
            Assert.Equal("Description", putProductRequest.Description);
            Assert.Equal("Company", putProductRequest.Company);
            Assert.Equal(1, putProductRequest.Price);
            Assert.Equal(1, putProductRequest.AgeRestriction);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PutProductRequest"/>
        /// it handles an incorrect capture of the product name.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectNameExpectFailure()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);

            putProductRequest.Name = "This is a product with a very long name which will make the model to break.";
            putProductRequest.Description = "Description";
            putProductRequest.Company = "Company";
            putProductRequest.Price = 1;
            putProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(putProductRequest, new ValidationContext(putProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PutProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product name must be of 50 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PutProductRequest"/>
        /// it handles an incorrect description length.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectDescriptionLengthExpectFailure()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);

            putProductRequest.Name = "Barby";
            putProductRequest.Description =
                "He my polite be object oh change. Consider no mr am overcame yourself throwing sociable children. Hastily her totally conduct may. My solid by stuff first smile fanny. Humoured how advanced mrs elegance sir who. Home sons when them dine do want to";
            putProductRequest.Company = "Company";
            putProductRequest.Price = 1;
            putProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(putProductRequest, new ValidationContext(putProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PutProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product description must be of 100 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PutProductRequest"/>
        /// it handles an incorrect age range.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectAgeRangeExpectFailure()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);

            putProductRequest.Name = "Barby";
            putProductRequest.Description = "Description";
            putProductRequest.Company = "Company";
            putProductRequest.Price = 1;
            putProductRequest.AgeRestriction = -1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(putProductRequest, new ValidationContext(putProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PutProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("Value for AgeRestriction must be between 0 and 100",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PutProductRequest"/>
        /// it handles an incorrect company length range.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectCompanyLengthExpectFailure()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);

            putProductRequest.Name = "Barby";
            putProductRequest.Description = "Description";
            putProductRequest.Company =
                "Prevailed sincerity behaviour to so do principle mr. As departure at no propriety zealously my. On dear rent if girl view. ";
            putProductRequest.Price = 1;
            putProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(putProductRequest, new ValidationContext(putProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PutProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product company must be of 50 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PutProductRequest"/>
        /// it handles an incorrect price range.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectPriceRangeExpectFailure()
        {
            var putProductRequest = new PutProductRequest();
            Assert.NotNull(putProductRequest);

            putProductRequest.Name = "Barby";
            putProductRequest.Description = "Description";
            putProductRequest.Company = "Mattel";
            putProductRequest.Price = 1001;
            putProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(putProductRequest, new ValidationContext(putProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PutProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("Price must be between $1 and $1000",
                validationResults[0].ErrorMessage);
        }
    }
}