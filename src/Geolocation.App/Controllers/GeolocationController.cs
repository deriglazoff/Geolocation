using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Dadata;
using Geolocation.Domain.Dto;

namespace Geolocation.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeolocationController : ControllerBase
    {
        private readonly ISuggestClientAsync _client;

        private readonly ILogger _logger;

        public GeolocationController(ILogger<GeolocationController> logger, ISuggestClientAsync clientAsync)
        {
            _logger = logger;
            _client = clientAsync;
        }

        /// <summary>
        /// Возвращает всю информацию об адресе по координатам. Работает для домов, улиц и городов.
        /// </summary>
        /// <param name="lat">Географическая широта</param>
        /// <param name="lon">Географическая долгота</param>
        [HttpGet]
        public async Task<IActionResult > Get(double lat, double lon)
        {
            var response = await _client.Geolocate(lat, lon);
            var result = response.suggestions.Select(c=> new AddressDto()
            {
                Value = c.value,
                Country = c.data.country,
                PostalCode = c.data.postal_code,
                KladrId = c.data.kladr_id,
                UnrestrictedValue = c.unrestricted_value
            });
            return Ok(result.FirstOrDefault());
        }
    }
}
