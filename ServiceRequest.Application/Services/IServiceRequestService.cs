using ServiceRequestManager.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.Services
{
    public interface IServiceRequestService
    {
        List<ServiceRequestDTO> GetAll();
        Task<ServiceRequestDTO> GetOneById(Guid id);
        Task<Guid> Create(ServiceRequestPostDTO serviceRequest);
    }
}
