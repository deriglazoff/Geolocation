using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dadata;
using Geolocation.Domain.Dto;
using Geolocation.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Geolocation.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeolocationController : ControllerBase
    {
        private readonly ISuggestClientAsync _client;

        private readonly IDbContextSqlServer _contextSql;

        public GeolocationController(ISuggestClientAsync clientAsync, IDbContextSqlServer contextSql)
        {
            _client = clientAsync;
            _contextSql = contextSql;
        }

        /// <summary>
        /// Возвращает всю информацию об адресе по координатам. Работает для домов, улиц и городов.
        /// </summary>
        /// <param name="lat">Географическая широта</param>
        /// <param name="lon">Географическая долгота</param>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(AddressDto),StatusCodes.Status200OK)]
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

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        [ProducesResponseType( StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckStateDatabase(string body)
        {
            await _contextSql.CheckStateDatabase(body);
            return Ok();
        }
    }
}
