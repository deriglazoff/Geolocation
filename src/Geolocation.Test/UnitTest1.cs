using System;
using System.Net.Http;
using Geolocation.Client;
using Serilog;
using Serilog.Formatting.Json;
using Xunit;

namespace Geolocation.Test
{
    public class Tests
    {

        public async void Test()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(new JsonFormatter(), "log.txt")
                .WriteTo.Console()
                .CreateLogger();

            var client = new Geolocation_Client(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            });

            try
            {
                await client.AddressesPOSTAsync(new AddressDto() { CorrelationId = new Guid() });
            }
            catch (Exception ex) when (
                ex is Exception
            )
            {
                var a = ex;
            }
        }
    }
}