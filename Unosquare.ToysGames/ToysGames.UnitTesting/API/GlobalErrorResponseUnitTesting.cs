using System;
using ToysGames.API.Models;
using Xunit;

namespace ToysGames.UnitTesting.API
{
    public class GlobalErrorResponseUnitTesting
    {
        /// <summary>
        /// This method tests the creation of a new GlobalErrorResponse instance.
        /// </summary>
        [Fact]
        public void CreateGlobalResponseInstanceExpectSuccess()
        {
            var globalResponse = new GlobalErrorResponse(new Exception("This is just a test exception"));
            Assert.NotNull(globalResponse);
            Assert.Equal("This is just a test exception", globalResponse.Message);
            Assert.Equal("Exception", globalResponse.Type);
        }
    }
}