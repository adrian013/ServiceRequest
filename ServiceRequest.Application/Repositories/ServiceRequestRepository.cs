using ServiceRequestManager.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        public Task<ServiceRequest> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
