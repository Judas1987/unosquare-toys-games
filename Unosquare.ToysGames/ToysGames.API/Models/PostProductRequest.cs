using System.ComponentModel.DataAnnotations;

namespace ToysGames.API.Models
{
    public class PostProductRequest
    {
        /// <summary>
        /// Represents the product name.
        /// </summary>
        [MaxLength(50, ErrorMessage = "The product name must be of 50 characters at maximum.")]
        [Required(ErrorMessage = "The product name is mandatory.")]
        public string Name { get; set; }

        /// <summary>
        /// Represents the product description.
        /// </summary>
        [MaxLength(100, ErrorMessage = "The product description must be of 100 characters at maximum.")]
        public string Description { get; set; }

        /// <summary>
        /// Represents the product age restriction.
        /// </summary>
        [Range(0, 100, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int? AgeRestriction { get; set; }

        /// <summary>
        /// Represents the product manufacturing company.
        /// </summary>
        [MaxLength(50, ErrorMessage = "The product company must be of 50 characters at maximum.")]
        [Required(ErrorMessage = "The product company is mandatory.")]
        public string Company { get; set; }

        /// <summary>
        /// Represents the product price.
        /// </summary>
        [Range(1, 1000, ErrorMessage = "{0} must be between ${1} and ${2}")]
        [Required(ErrorMessage = "The product price is mandatory.")]
        public double? Price { get; set; }
    }
}