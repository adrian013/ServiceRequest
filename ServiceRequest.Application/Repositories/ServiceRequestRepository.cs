﻿using Microsoft.EntityFrameworkCore;
using ServiceRequestManager.Application.DataContext;
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
        private ServiceRequestDBContext _context;
        public ServiceRequestRepository(ServiceRequestDBContext context)
        {
            _context = context;
        }
        public List<ServiceRequest> GetAll()
        {
            return _context.ServiceRequest.ToList();
        }

        public async Task<ServiceRequest> GetOneById(Guid id)
        {
            return await _context.ServiceRequest.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Guid> Create(ServiceRequest serviceRequest)
        {
            serviceRequest.Id = Guid.NewGuid();

            _context.ServiceRequest.Add(serviceRequest);
            await _context.SaveChangesAsync();

            return serviceRequest.Id;
        }
    }
}
