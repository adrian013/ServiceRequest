using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.DataContext
{
    public class ServiceRequestDBContext : DbContext
    {
        public ServiceRequestDBContext(DbContextOptions<ServiceRequestDBContext> options) : base(options)
        {
        }

        public DbSet<Model.ServiceRequest> ServiceRequest { get; set; }
    }
}
