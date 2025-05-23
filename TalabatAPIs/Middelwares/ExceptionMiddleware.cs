using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using TalabatAPIs.Errors;

namespace TalabatAPIs.Middelwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env) 
        {

            this.next = next;
            this.logger = logger;
            this.env = env;
        }


        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
               await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode= StatusCodes.Status500InternalServerError;

                var response = env.IsDevelopment() ? new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex?.StackTrace?.ToString()) : new ApiExceptionResponse(StatusCodes.Status500InternalServerError);

                var option = new JsonSerializerOptions () { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response);

               await context.Response.WriteAsync(json);

                throw;
            }
        }
    
    
    }
    
    
}
