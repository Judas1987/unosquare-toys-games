using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToysGames.API.Exceptions;
using ToysGames.API.Interfaces;
using ToysGames.API.Models;
using ToysGames.Data.Models;

namespace ToysGames.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// This method serves the request to the products resource of this API.
        /// </summary>
        /// <param name="request">An instance of <see cref="PostProductRequest"/></param>
        /// <returns>The result of this action.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] PostProductRequest request)
        {
            try
            {
                _logger.LogDebug($"A new request has been received");
                _logger.LogDebug(JsonConvert.SerializeObject(request));

                var newProduct = new Product(Guid.NewGuid(), request.Name, request.Description, request.AgeRestriction,
                    request.Company, request.Price);

                _unitOfWork.Products.Insert(newProduct);
                _unitOfWork.Commit();

                _logger.LogDebug("The new product has been successfully created.");

                return new OkObjectResult(new BaseResponse<Product>
                {
                    Data = new List<Product> {newProduct}
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception while processing request to add new product.");
                _logger.LogCritical($"Exception message: {e.Message}");

                throw;
            }
            finally
            {
                _logger.LogDebug("The request for the products resource has been finished.");
            }
        }

        /// <summary>
        /// This method handles the GET requests to the product resource.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(new BaseResponse<Product>
            {
                Data = _unitOfWork.Products.Get().ToList()
            });
        }

        /// <summary>
        /// This method handles the GET request for the product resource for specific data.
        /// </summary>
        /// <param name="productId">Represents the product id.</param>
        /// <returns>The existing <see cref="Product"/> in case that exist.</returns>
        /// <exception cref="EntityNotFoundException">This exception is thrown in case that the id does not exist.</exception>
        [HttpGet("{productId}")]
        public IActionResult GetById(string productId)
        {
            try
            {
                if (!Guid.TryParse(productId, out var parsedProductId))
                    throw new BadRequestException($"The product id {productId} cannot be parsed correctly.");

                Product existingProduct = _unitOfWork.Products
                    .Get(product => product.ProductId == parsedProductId)
                    .SingleOrDefault();

                if (existingProduct == null)
                    throw new EntityNotFoundException($"The productId {productId} does not exist in the database.");

                return new OkObjectResult(new BaseResponse<Product>
                {
                    Data = new List<Product> {existingProduct}
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception while processing request to add new product.");
                _logger.LogCritical($"Exception message: {e.Message}");

                throw;
            }
            finally
            {
                _logger.LogDebug("The request for the products resource has been finished.");
            }
        }

        /// <summary>
        /// This method handles the PUT requests to the product resource.
        /// </summary>
        /// <param name="newProductData">The product information to be update.</param>
        /// <param name="productId">Represents the product id.</param>
        /// <returns>The updated <see cref="Product"/></returns>
        [HttpPut("{productId}")]
        public IActionResult Put([FromBody] PutProductRequest newProductData, string productId)
        {
            try
            {
                bool commitRequired = false;

                if (!Guid.TryParse(productId, out var parsedProductId))
                    throw new BadRequestException($"The product id {productId} cannot be parsed correctly.");

                Product existingProduct = _unitOfWork.Products
                    .Get(product => product.ProductId == parsedProductId)
                    .SingleOrDefault();

                if (existingProduct == null)
                    throw new EntityNotFoundException($"The productId {productId} does not exist in the database.");

                if (!string.IsNullOrEmpty(newProductData.Name) && newProductData.Name != existingProduct.Name)
                {
                    existingProduct.Name = newProductData.Name;
                    commitRequired = true;
                }

                if (!string.IsNullOrEmpty(newProductData.Description) &&
                    newProductData.Description != existingProduct.Description)
                {
                    existingProduct.Description = newProductData.Description;
                    commitRequired = true;
                }

                if (newProductData.AgeRestriction != null &&
                    newProductData.AgeRestriction != existingProduct.AgeRestriction)
                {
                    existingProduct.AgeRestriction = newProductData.AgeRestriction;
                    commitRequired = true;
                }

                if (!string.IsNullOrEmpty(newProductData.Company) && newProductData.Company != existingProduct.Company)
                {
                    existingProduct.Company = newProductData.Company;
                    commitRequired = true;
                }

                if (newProductData.Price != null && !newProductData.Price.Equals(existingProduct.Price))
                {
                    existingProduct.Price = newProductData.Price;
                    commitRequired = true;
                }

                if (commitRequired)
                {
                    _unitOfWork.Products.Update(existingProduct);
                    _unitOfWork.Commit();
                }

                return new OkObjectResult(new BaseResponse<Product>
                {
                    Data = new List<Product> {existingProduct}
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception while processing request to add new product.");
                _logger.LogCritical($"Exception message: {e.Message}");

                throw;
            }
            finally
            {
                _logger.LogDebug("The request for the products resource has been finished.");
            }
        }

        /// <summary>
        /// This method handles the DELETE requests for the product resource.
        /// </summary>
        /// <param name="productId">Represents the product id.</param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public IActionResult Delete(string productId)
        {
            try
            {
                if (!Guid.TryParse(productId, out var parsedProductId))
                    throw new BadRequestException($"The product id {productId} cannot be parsed correctly.");

                Product existingProduct = _unitOfWork.Products
                    .Get(product => product.ProductId == parsedProductId)
                    .SingleOrDefault();

                if (existingProduct == null)
                    throw new EntityNotFoundException($"The productId {productId} does not exist in the database.");

                _unitOfWork.Products.Delete(existingProduct);
                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogCritical("Exception while processing request to delete product.");
                _logger.LogCritical($"Exception message: {e.Message}");

                throw;
            }
            finally
            {
                _logger.LogDebug("The request for the products resource has been finished.");
            }
        }
    }
}