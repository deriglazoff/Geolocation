using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Geolocation.App.Filter
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogWarning(exception, "Îøèáêà");
            context.Response.StatusCode = exception switch
            {
                BadHttpRequestException => (int) HttpStatusCode.BadRequest,
                _ => (int) HttpStatusCode.InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Type = exception.GetType().FullName,
                Title = exception.Message,
            };

            await context.Response.WriteAsJsonAsync(problemDetails).ConfigureAwait(false);
        }
    }


}
