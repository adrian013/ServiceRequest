using Microsoft.Extensions.DependencyInjection;
using ServiceRequestManager.Application.Repositories;
using ServiceRequestManager.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddTransient<IServiceRequestService, ServiceRequestService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IServiceRequestRepository, ServiceRequestRepository>();
            return services;
        }
    }
}
