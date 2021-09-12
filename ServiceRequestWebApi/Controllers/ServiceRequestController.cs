using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceRequestManager.Application.Services;
using ServiceRequestManager.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// Returns all the service requests.
        /// </summary>
        /// <returns>A list of ServiceRequest</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<ServiceRequestDTO>> GetAllServiceRequest()
        {
            var serviceRequests =  _serviceRequestService.GetAll();

            if (serviceRequests == null)
            {
                return NoContent();
            }

            return Ok(serviceRequests);
        }
    }
}
