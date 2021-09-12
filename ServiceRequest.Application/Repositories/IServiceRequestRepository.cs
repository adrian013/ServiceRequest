using ServiceRequestManager.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<ServiceRequest> GetOneById(Guid id);
        List<ServiceRequest> GetAll();
        Task<Guid> Create(ServiceRequest serviceRequest);
    }
}
