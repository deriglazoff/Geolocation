using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Geolocation.Domain.Dto;
using Geolocation.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Geolocation.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressesController : ControllerBase
    {
        private readonly IRepository<IAddress> _context;

        private readonly IRunner _job;
        private readonly ILogger<AddressesController> _logger;

        public AddressesController(IRepository<IAddress> context, IRunner job, ILogger<AddressesController> logger)
        {
            _context = context;
            _job = job;
            _logger = logger;
        }

        /// <summary>
        /// Список адресов.
        /// </summary>
        [HttpGet]
        [ProducesDefaultResponseType(typeof(ProblemDetails))]
        [ProducesResponseType(typeof(IEnumerable<IAddress>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            _logger.LogWarning("Use get {@User}", HttpContext.User?.Identity);
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
