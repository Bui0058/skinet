using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, 
                            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }
        //the middleware message
        public async Task InvokeAsync(HttpContext context)
        {
            try{
                await _next(context); //if no exception the request go to next middleware
            } catch (Exception ex){ //there is exception, use our Middleware
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response  = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) 
                    : new ApiException((int)HttpStatusCode.InternalServerError) ; 
                
                //make sure json respone in camelCase consistent with other error response
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
               
                var json = JsonSerializer.Serialize(response, options); //serialize response to json format
                await context.Response.WriteAsync(json);
            }
        }
    }
}