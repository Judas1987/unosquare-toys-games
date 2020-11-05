using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using ToysGames.API.Controllers;
using ToysGames.API.Interfaces;
using ToysGames.Data.Models;

namespace ToysGames.UnitTesting.Configuration
{
    public class ApiWebApplicationFactory : WebApplicationFactory<ToysGames.API.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var mockedLogger = new Mock<ILogger<ProductsController>>();
                var mockedUnitOfWork = new Mock<IUnitOfWork>();
                //services.AddSingleton(mockedUnitOfWork.Object);
                //services.AddSingleton(mockedLogger.Object);
            });

            //base.ConfigureWebHost(builder);
        }
    }
}