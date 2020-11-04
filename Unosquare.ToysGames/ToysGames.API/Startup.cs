using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToysGames.API.Exceptions;
using ToysGames.API.Interfaces;
using ToysGames.API.Workers;
using ToysGames.Data;
using ToysGames.Data.Models;

namespace ToysGames.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("TestDatabase"));

            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase("Products")
                .Options;
            var databaseContext = new ProductContext(options);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(databaseContext);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/error");
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            


            var unitOfWork = app.ApplicationServices.GetService<IUnitOfWork>();

            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Barby", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Ken", "This is a Ken.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Winnie the poo", "This is a barby.", 1, "Mattel",
                123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Goku", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Vegeta", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "King kong", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Smurfs set", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Lord of the ring", "This is a barby.", 1, "Mattel",
                123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Barby II", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Barby III", "This is a barby.", 1, "Mattel", 123));
            unitOfWork.Products.Insert(new Product(Guid.NewGuid(), "Barby IV", "This is a barby.", 1, "Mattel", 123));

            unitOfWork.Commit();
        }
    }
}