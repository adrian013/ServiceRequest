using AutoMapper;
using ServiceRequestManager.Application.DTO;
using ServiceRequestManager.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceRequestManager.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ServiceRequest, ServiceRequestDTO>();
            CreateMap<ServiceRequestPostDTO, ServiceRequest>();
        }
    }
}
