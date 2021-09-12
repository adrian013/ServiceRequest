using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceRequestManager.Application.Services;
using ServiceRequestManager.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceRequestManager.Application.Exceptions;

namespace ServiceRequestManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        /// <summary>
        /// Read all service requests.
        /// </summary>
        /// <returns>A list of ServiceRequest</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ServiceRequestDTO>> GetAll()
        {
            var serviceRequests =  _serviceRequestService.GetAll();

            if (serviceRequests == null)
            {
                return NoContent();
            }

            return Ok(serviceRequests);
        }

        /// <summary>
        /// Read service request by id.
        /// </summary>
        /// <returns>A ServiceRequest inscance</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceRequestDTO>> GetById(Guid id)
        {
            var serviceRequest = await _serviceRequestService.GetOneById(id);

            if (serviceRequest == null)
            {
                return NotFound();
            }

            return Ok(serviceRequest);
        }

        /// <summary>
        /// Create new service request.
        /// </summary>
        /// <returns>Status code</returns>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Post([FromBody] ServiceRequestPostDTO body)
        {
            if (!Enum.IsDefined(typeof(Enums.CurrentStatus), body.CurrentStatus))
                return BadRequest("Invalid Status");

            var id = await _serviceRequestService.Create(body);

            return Created($"api/serviceRequest/{id}", id);
        }

        /// <summary>
        /// update service request based on id
        /// </summary>
        /// <returns>Status code</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> Put([FromRoute] Guid id, [FromBody] ServiceRequestPostDTO body)
        {
            if (!Enum.IsDefined(typeof(Enums.CurrentStatus), body.CurrentStatus))
                return BadRequest("Invalid Status");

            try
            {
                await _serviceRequestService.Update(body, id);
            }
            catch (NotFoundExeption)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
