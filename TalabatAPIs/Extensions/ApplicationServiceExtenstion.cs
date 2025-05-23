using Microsoft.EntityFrameworkCore;
using Talabat.Core.Service.Contract;
using Talabat.Core;
using Talabat.Repository;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.Services.Products;
using Talabat.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using TalabatAPIs.Errors;
using StackExchange.Redis;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Repositories;
using Talabat.Core.Mapping.Basket;
using Talabat.Service.Services.Cache;
using Talabat.Repository.Identity.Context;
using Talabat.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Talabat.Service.Services.User;
using Talabat.Service.Services.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace TalabatAPIs.Extensions
{
    public static class ApplicationServiceExtenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInServices();

            services.AddBuiltInServices();
            services.AddSwaggerServices();
            services.AddDbContextServices(configuration);
            services.AddUserDefinedServices();
            services.AddAutoMapperServices(configuration);
            services.ConfigureInvalidStateResponseServices(configuration);
            services.AddRedisServices(configuration);
            services.AddIdentityServices();
            services.AddAuthenticationServices(configuration);
            return services;
        }
        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
         {
            services.AddControllers(); // Register Required WebAPIs Services To DI Container
            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


        private static IServiceCollection AddDbContextServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            return services;
        }


        private static IServiceCollection AddUserDefinedServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
           

            return services;
        }


        private static IServiceCollection AddAutoMapperServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));

            return services;
        }

        private static IServiceCollection ConfigureInvalidStateResponseServices(this IServiceCollection services, IConfiguration configuration)
        {

           services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                                              .SelectMany(P => P.Value.Errors)
                                              .Select(E => E.ErrorMessage)
                                              .ToArray();

                    var validationErrorsResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorsResponse);
                };

            });

            return services;
        }

        private static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                
                return ConnectionMultiplexer.Connect(connection);
            });

            return services;

        }


        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;

        }

        private static IServiceCollection AddAuthenticationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                };
            });
            return services;

        }

    }
}
