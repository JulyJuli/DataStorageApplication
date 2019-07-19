using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DataStorageApplication.WebApi.ExceptionMiddlewares
{
    public class InvalidExtentionExceptionMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public InvalidExtentionExceptionMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate.Invoke(context);
            }
            catch (InvalidDataException exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, InvalidDataException exception)
        {
            var response = context.Response;
            var statusCode = (int)HttpStatusCode.InternalServerError;

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject( exception.Message));
        }
    }
}
