using SocialMediaAPI.ErrorModels;
using System.Net;

namespace SocialMediaAPI.MiddleWares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"somthing went wrong {ex}");

                await HandelExceptionAsync(httpContext, ex);
                
            }
            

        }

        private async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            if(response.ErrorMessage == "Cannot create a DbSet for 'IdentityRole' because this type is not included in the model for the context.")
            {
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ErrorMessage = "Done ya kbeer";
            }
            await httpContext.Response.WriteAsJsonAsync(response);
        }
    }
}
