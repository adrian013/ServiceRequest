using ServiceRequestManager.Application.Repositories;
using ServiceRequestManager.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace ServiceRequestManager.Application.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly IMapper _mapper;

        public ServiceRequestService(IServiceRequestRepository serviceRequestRepository, IMapper mapper)
        {
            _serviceRequestRepository = serviceRequestRepository;
            _mapper = mapper;
        }

        public List<ServiceRequestDTO> GetAll()
        {
            var serviceRequests = _serviceRequestRepository.GetAll();

            return _mapper.Map<List<ServiceRequestDTO>>(serviceRequests);
        }

        public async Task<ServiceRequestDTO> GetOneById(Guid id)
        {
            var serviceRequest = await _serviceRequestRepository.GetOneById(id);

            return _mapper.Map<ServiceRequestDTO>(serviceRequest);
        }
    }
}
