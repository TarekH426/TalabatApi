using System.Runtime.CompilerServices;
using Talabat.Repository.Data.Contexts;
using Talabat.Repository.Data;
using Microsoft.EntityFrameworkCore;
using TalabatAPIs.Middelwares;
using Talabat.Repository.Identity.Context;
using Talabat.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace TalabatAPIs.Extensions
{
    public static class ConfigureMiddlewares
    {
        public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>(); // Ask CLR For Creating Object From DbContext Explicitly
            var identityContext = services.GetRequiredService<StoreIdentityDbContext>();// Ask CLR For Creating Object From StoreIdentityDbContext Explicitly
            var userManager = services.GetRequiredService<UserManager<AppUser>>();// Ask CLR For Creating Object From StoreIdentityDbContext Explicitly

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync(); // Update Database
                await StoreContextSeed.SeedAsync(_dbContext); // Data Seeding
               await identityContext.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Apply The Migration ");

            }

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app; 
        }
    }
}
