using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ServiceRequestManager.Application.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.Model
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ServiceRequestDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<ServiceRequestDBContext>>()))
            {
                if (context.ServiceRequest.Any())
                {
                    return;   // Database has been seeded
                }

                context.ServiceRequest.AddRange(
                    new ServiceRequest
                    {
                        Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                        BuildingCode= "COH",
                        Description= "Please turn up the AC in suite 1200D. It is too hot here.",
                        CurrentStatus= "Created",
                        CreatedBy= "Nik Patel",
                        CreatedDate= new DateTime(2019,08,01,14,25,43,511),
                        LastModifiedBy= "Jane Doe",
                        LastModifiedDate= new DateTime(2019,08,01,15,01,23,511)
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
