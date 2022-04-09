using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Geolocation.App.Filter
{
    /// <summary>
    /// Middleware для обработки ошибок
    /// </summary>
    internal class HttpRequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public HttpRequestLoggingMiddleware(RequestDelegate next, ILogger<HttpRequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await LogHttpMessages(context).ConfigureAwait(false);
        }

        private async Task LogHttpMessages(HttpContext context)
        {
            var logEnrich = await GetHttpContextLogger(context);

            var originalResponseBodyStream = context.Response.Body;
            try
            {
                _logger.LogDebug("{Host} {QueryString} {ClientAgent} Request body {ContentType} {Request}",
                    logEnrich.Host, logEnrich.ContentType, logEnrich.QueryString,
                    logEnrich.ClientAgent, logEnrich.InputBody);

                await using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;

                await _next.Invoke(context).ConfigureAwait(false);

                _logger.LogInformation("Sending response {Method} {Path}. Status code {StatusCode}",
                    context.Request.Method, context.Request.Path, context.Response.StatusCode);

                var response = await GetBody(context.Response.Body);

                _logger.LogDebug("Response body {Response}", response);

                await responseBody.CopyToAsync(originalResponseBodyStream).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new CoreHttpException(ex, logEnrich);
            }
            finally
            {
                context.Response.Body = originalResponseBodyStream;
            }

        }


        private async Task<HttpContextLogger> GetHttpContextLogger(HttpContext context)
        {
            context.Request.EnableBuffering();

            var body = await GetBody(context.Request.Body);

            return new HttpContextLogger()
            {
                InputBody = body,
                Host = context.Request.Host.ToString(),
                Method = context.Request.Method,
                ContentType = context.Request.ContentType,
                Path = context.Request.Path,
                QueryString = context.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
                CorrelationId = context.Request.Headers["X-Correlation-ID"],
                ClientAgent = context.Request.Headers["User-Agent"]

            };
        }

        private async Task<string> GetBody(Stream body)
        {
            body.Seek(0, SeekOrigin.Begin);
            using var streamReader = new StreamReader(body, leaveOpen: true);
            var responseText = await streamReader.ReadToEndAsync().ConfigureAwait(false);
            body.Seek(0, SeekOrigin.Begin);
            return responseText;
        }

        public class HttpContextLogger
        {
            public string InputBody;
            public string Path { get; set; }
            public string Method { get; set; }
            public string ContentType { get; set; }
            public string Host { get; set; }

            public Dictionary<string, string> QueryString { get; set; }
            public string CorrelationId { get; set; }
            public string ClientAgent { get; set; }

        }

        public sealed class CoreHttpException : Exception
        {
            public string InputBody { get; set; }
            public HttpContextLogger HttpContextLogger { get; }

            public CoreHttpException(Exception exception, HttpContextLogger httpContextLogger) : base(
                exception.Message, exception)
            {
                HttpContextLogger = httpContextLogger;
                InputBody = httpContextLogger.InputBody;
            }


        }

    }
}