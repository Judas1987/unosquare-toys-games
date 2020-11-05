using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ToysGames.API.Models
{
    public class BaseResponse<T> where T : class
    {
        /// <summary>
        /// Represents the list of a give data.
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Represents the count of the data.
        /// </summary>
        public int Count => Data?.Count ?? 0;

        /// <summary>
        /// Represents the total items available in this response.
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Represents the current index.
        /// </summary>
        public int CurrentIndex { get; set; }

        /// <summary>
        /// Represents the total available items in the database.
        /// </summary>
        public int AvailableItems { get; set; }
    }
}