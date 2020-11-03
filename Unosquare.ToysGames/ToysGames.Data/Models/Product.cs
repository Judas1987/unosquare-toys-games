using System;
using System.ComponentModel.DataAnnotations;

namespace ToysGames.Data.Models
{
    /// <summary>
    /// Represents a product with its properties and functions/methods.
    /// </summary>
    public class Product
    {
        #region Private members

        private readonly int _id;
        private Guid _productId;
        private string _name;
        private string _description;
        private int? _ageRestriction;
        private string _company;
        private double? _price;

        #endregion

        /// <summary>
        /// This is the private constructor utilized by EF to initialize the entity.
        /// </summary>
        /// <param name="id">Represents the private product id.</param>
        /// <param name="productId">Represents the public product id.</param>
        /// <param name="name">Represents the product name.</param>
        /// <param name="description">Represents the product description.</param>
        /// <param name="ageRestriction">Represents the age restriction.</param>
        /// <param name="company">Represents the product manufacturing company.</param>
        /// <param name="price">Represents the product price.</param>
        private Product(int id, Guid productId, string name, string description, int ageRestriction, string company,
            double? price)
        {
            _id = id;
            _productId = productId;
            _name = name;
            _description = description;
            _ageRestriction = ageRestriction;
            _company = company;
            _price = price;
        }

        /// <summary>
        /// Initializes a new instance of the product entity class.
        /// </summary>
        /// <param name="productId">Represents the public product id.</param>
        /// <param name="name">Represents the product name.</param>
        /// <param name="description">Represents the product description.</param>
        /// <param name="ageRestriction">Represents the age restriction.</param>
        /// <param name="company">Represents the product manufacturing company.</param>
        /// <param name="price">Represents the product price.</param>
        public Product(Guid productId, string name, string description, int? ageRestriction, string company,
            double? price)
        {
            _productId = productId;
            _name = name;
            _description = description;
            _ageRestriction = ageRestriction;
            _company = company;
            _price = price;
        }

        /// <summary>
        /// Represents the product id which can be seen by the public. For security purposes the numeric id
        /// should never be available to be seen by the public.
        /// </summary>
        public Guid ProductId => _productId;

        /// <summary>
        /// Represents the product name.
        /// </summary>
        [MaxLength(50, ErrorMessage = "The product name must be of 50 characters at maximum.")]
        [Required(ErrorMessage = "The product name is mandatory.")]
        public string Name => _name;

        /// <summary>
        /// Represents the product description.
        /// </summary>
        [MaxLength(100, ErrorMessage = "The product description must be of 100 characters at maximum.")]
        public string Description => _description;

        /// <summary>
        /// Represents the product age restriction.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int? AgeRestriction => _ageRestriction;

        /// <summary>
        /// Represents the product manufacturing company.
        /// </summary>
        [MaxLength(50, ErrorMessage = "The product company must be of 50 characters at maximum.")]
        [Required(ErrorMessage = "The product company is mandatory.")]
        public string Company => _company;

        /// <summary>
        /// Represents the product price.
        /// </summary>
        [Range(1, 1000, ErrorMessage = "{0} must be between ${1} and ${2}")]
        public double? Price => _price;
    }
}