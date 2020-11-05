using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToysGames.API.Models;
using Xunit;

namespace ToysGames.UnitTesting.API
{
    public class PostProductRequestUnitTest
    {
        /// <summary>
        /// This method creates a new instance of the post product request class.
        /// </summary>
        [Fact]
        public void CreateNewInstanceExpectSuccess()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);
        }

        /// <summary>
        /// This method tests that the data assigned in the class does not get transformed inside of the object.
        /// </summary>
        [Fact]
        public void CreateInstanceAndAssignDataExpectSuccess()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Name";
            postProductRequest.Description = "Description";
            postProductRequest.Company = "Company";
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = 1;

            Assert.Equal("Name", postProductRequest.Name);
            Assert.Equal("Description", postProductRequest.Description);
            Assert.Equal("Company", postProductRequest.Company);
            Assert.Equal(1, postProductRequest.Price);
            Assert.Equal(1, postProductRequest.AgeRestriction);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles an incorrect capture of the product name.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectNameExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "This is a product with a very long name which will make the model to break.";
            postProductRequest.Description = "Description";
            postProductRequest.Company = "Company";
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product name must be of 50 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles an incorrect capture of the product name.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestMissingNameExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = null;
            postProductRequest.Description = "Description";
            postProductRequest.Company = "Company";
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product name is mandatory.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles an incorrect description length.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectDescriptionLengthExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Barby";
            postProductRequest.Description =
                "He my polite be object oh change. Consider no mr am overcame yourself throwing sociable children. Hastily her totally conduct may. My solid by stuff first smile fanny. Humoured how advanced mrs elegance sir who. Home sons when them dine do want to";
            postProductRequest.Company = "Company";
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product description must be of 100 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles an incorrect age range.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectAgeRangeExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Barby";
            postProductRequest.Description = "Description";
            postProductRequest.Company = "Company";
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = -1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("Value for AgeRestriction must be between 0 and 100",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles an incorrect company length range.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectCompanyLengthExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Barby";
            postProductRequest.Description = "Description";
            postProductRequest.Company =
                "Prevailed sincerity behaviour to so do principle mr. As departure at no propriety zealously my. On dear rent if girl view. ";
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product company must be of 50 characters at maximum.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles a missing company name.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestMissingCompanyExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Barby";
            postProductRequest.Description = "Description";
            postProductRequest.Company = null;
            postProductRequest.Price = 1;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product company is mandatory.",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles an incorrect price range.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestIncorrectPriceRangeExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Barby";
            postProductRequest.Description = "Description";
            postProductRequest.Company = "Mattel";
            postProductRequest.Price = 1001;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("Price must be between $1 and $1000",
                validationResults[0].ErrorMessage);
        }

        /// <summary>
        /// This method tests the creation of an instance of <see cref="PostProductRequest"/>
        /// it handles a missing price.
        /// </summary>
        [Fact]
        public void ValidatePostProductRequestMissingPriceExpectFailure()
        {
            var postProductRequest = new PostProductRequest();
            Assert.NotNull(postProductRequest);

            postProductRequest.Name = "Barby";
            postProductRequest.Description = "Description";
            postProductRequest.Company = "Mattel";
            postProductRequest.Price = null;
            postProductRequest.AgeRestriction = 1;

            var validationResults = new List<ValidationResult>();
            bool isValid =
                Validator.TryValidateObject(postProductRequest, new ValidationContext(postProductRequest),
                    validationResults, true);

            Assert.False(isValid, "The PostProductRequest instance passed the validations.");
            Assert.True(validationResults.Count == 1, "The validation results are not equal to one.");
            Assert.Equal("The product price is mandatory.", validationResults[0].ErrorMessage);
        }
    }
}