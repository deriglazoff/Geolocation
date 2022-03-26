using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Geolocation.Domain.Dto;
using Geolocation.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Geolocation.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IGeolocationContext _context;


        public AddressesController(IGeolocationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Список адресов.
        /// </summary>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(IEnumerable<IAddress>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Get();
            return Ok(result);
        }
        /// <summary>
        /// Список адресов.
        /// </summary>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Insert(AddressDto address)
        {
            await _context.Insert(address);
            return Ok();
        }
    }
}
