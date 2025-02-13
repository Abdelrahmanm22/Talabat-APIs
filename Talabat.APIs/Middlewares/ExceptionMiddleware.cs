using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> looger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate Next,ILogger<ExceptionMiddleware> looger,IHostEnvironment env)
        {
            next = Next;
            this.looger = looger;
            this.env = env;
        }
        // InvokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex) {
                looger.LogError(ex,ex.Message);

                //Production ==>> Log ex in Database

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                //if (env.IsDevelopment())
                //{
                //    var Response = new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiExceptionResponse(500);
                //} 

                var Response = env.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse(500);
                var Options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                context.Response.WriteAsync(JsonSerializer.Serialize(Response, Options));
            }
        }

    }
}
