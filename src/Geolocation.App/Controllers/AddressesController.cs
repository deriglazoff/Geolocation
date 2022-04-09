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
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IRepository<IAddress> _context;

        private readonly IRunner _job;
        public AddressesController(IRepository<IAddress> context, IRunner job)
        {
            _context = context;
            _job = job;
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

        /// <summary>
        /// Список адресов.
        /// </summary>
        [HttpDelete]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Remove()
        {
            Response.OnCompleted(async () => await _job.Run());
            return Ok();
        }
    }
}
