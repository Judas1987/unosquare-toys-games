using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using ToysGames.API.Controllers;
using ToysGames.API.Interfaces;
using ToysGames.API.Models;
using ToysGames.Data.Models;
using Xunit;
using Xunit.Abstractions;

namespace ToysGames.UnitTesting.API
{
    public class ProductsControllerUnitTest : IClassFixture<WebApplicationFactory<ToysGames.API.Startup>>
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly HttpClient _client;

        public ProductsControllerUnitTest(ITestOutputHelper testOutputHelper,
            WebApplicationFactory<ToysGames.API.Startup> fixture)
        {
            _testOutputHelper = testOutputHelper;
            _client = fixture.CreateClient();
        }

        /// <summary>
        /// This test method creates a new instance of the product controller and verifies that the instance is not null.
        /// </summary>
        [Fact]
        public void CreateControllerInstanceExpectSuccess()
        {
            var mockedLogger = new Mock<ILogger<ProductsController>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var controller = new ProductsController(mockedLogger.Object, mockedUnitOfWork.Object);

            Assert.NotNull(controller);
        }

        /// <summary>
        /// This test method creates a new product by calling the controller.
        /// </summary>
        [Fact]
        public async void PostProductExpectSuccess()
        {
            var request = new PostProductRequest
            {
                Name = "A test product",
                Description = "Description",
                Company = "Company",
                Price = 10,
                AgeRestriction = 1
            };

            var buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(request));
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = await _client
                .PostAsync("/products", byteContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseContent);

            BaseResponse<Product> createdProductResponse =
                JsonConvert.DeserializeObject<BaseResponse<Product>>(responseContent);

            Assert.NotNull(createdProductResponse);
            Assert.Equal(1, createdProductResponse.Count);

            var createdProduct = createdProductResponse.Data.SingleOrDefault();

            Assert.NotNull(createdProduct);
            Assert.NotEmpty(createdProduct.ProductId.ToString());
        }

        [Fact]
        public async void GetProductExpectSuccess()
        {
            var response = await _client.GetAsync("/products");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            string responseContent = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(responseContent);

            BaseResponse<Product> createdProductResponse =
                JsonConvert.DeserializeObject<BaseResponse<Product>>(responseContent);
            
            Assert.NotNull(createdProductResponse);
            Assert.Equal(3, createdProductResponse.Count);
        }
    }
}