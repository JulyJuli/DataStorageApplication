using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace DataStorageApplication.WebApi.ExceptionMiddlewares
{
    public class ExceptionMiddlewareBase
    {
        private readonly RequestDelegate requestDelegate;

        public ExceptionMiddlewareBase(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate.Invoke(context);
            }
            catch (Exception exception)
            {
                if (exception is InvalidDataException || exception is FileNotFoundException || exception is Exception)
                {
                    await HandleExceptionAsync(context, exception, HttpStatusCode.BadRequest);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            var response = context.Response;
            var statusCode = (int)httpStatusCode;

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(exception.Message));
        }
    }
}
