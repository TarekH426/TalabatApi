
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.Core;
using Talabat.Core.Mapping.Products;
using Talabat.Core.Service.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.Services.Products;
using TalabatAPIs.Errors;
using TalabatAPIs.Extensions;
using TalabatAPIs.Middelwares;

namespace TalabatAPIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
        
            var webAppBuilder = WebApplication.CreateBuilder(args);

            #region Configure services
            // Add services to the DI container.


            webAppBuilder.Services.AddApplicationServices(webAppBuilder.Configuration);






            #endregion

            var app = webAppBuilder.Build();

            // Create Scope

         


            #region Configure Kestrel Middelewares

             await app.ConfigureMiddlewaresAsync();

            #endregion

            app.Run();
        }
    }
}
