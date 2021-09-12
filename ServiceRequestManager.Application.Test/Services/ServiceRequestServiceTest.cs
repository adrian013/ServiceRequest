using AutoMapper;
using Moq;
using ServiceRequestManager.Application.DTO;
using ServiceRequestManager.Application.Model;
using ServiceRequestManager.Application.Profiles;
using ServiceRequestManager.Application.Repositories;
using ServiceRequestManager.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ServiceRequestManager.Application.Test.Services
{
    public class ServiceRequestServiceTest
    {
        private readonly Mock<IServiceRequestRepository> _serviceRequestRepositoryMock;
        private readonly IMapper _mapper;

        public ServiceRequestServiceTest()
        {
            _serviceRequestRepositoryMock = new Mock<IServiceRequestRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        #region getAll

        [Fact]
        public void GetAll_Returns_ServiceRequests_If_Exists()
        {
            var fakeServiceRequestsDTO = new List<ServiceRequestDTO>
            {
                new ServiceRequestDTO
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                    BuildingCode = "Fake Building Code",
                    CreatedBy = "Fake User",
                    CreatedDate = new DateTime(2020, 1, 1),
                    LastModifiedBy = "Fake User",
                    LastModifiedDate = new DateTime(2020, 2, 1),
                    CurrentStatus = Enums.CurrentStatus.Complete,
                    Description = "Fake Description"
                },
                new ServiceRequestDTO
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"),
                    BuildingCode = "Fake Building Code 2",
                    CreatedBy = "Fake User 2",
                    CreatedDate = new DateTime(2020, 1, 1),
                    LastModifiedBy = "Fake User 2",
                    LastModifiedDate = new DateTime(2020, 2, 1),
                    CurrentStatus = Enums.CurrentStatus.Complete,
                    Description = "Fake Description 2"
                }
            };

            var fakeServiceRequests = new List<ServiceRequest>
            {
                new ServiceRequest
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                    BuildingCode = "Fake Building Code",
                    CreatedBy = "Fake User",
                    CreatedDate = new DateTime(2020, 1, 1),
                    LastModifiedBy = "Fake User",
                    LastModifiedDate = new DateTime(2020, 2, 1),
                    CurrentStatus = "Complete",
                    Description = "Fake Description"
                },
                new ServiceRequest
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"),
                    BuildingCode = "Fake Building Code 2",
                    CreatedBy = "Fake User 2",
                    CreatedDate = new DateTime(2020, 1, 1),
                    LastModifiedBy = "Fake User 2",
                    LastModifiedDate = new DateTime(2020, 2, 1),
                    CurrentStatus = "Complete",
                    Description = "Fake Description 2"
                }
            };

            _serviceRequestRepositoryMock.Setup(m => m.GetAll()).Returns(fakeServiceRequests);

            var service = new ServiceRequestService(_serviceRequestRepositoryMock.Object, _mapper);

            var result = service.GetAll();

            Assert.NotNull(result);

            string fakeSerialized = JsonSerializer.Serialize(fakeServiceRequestsDTO);
            string resultSerialized = JsonSerializer.Serialize(result);

            Assert.Equal(fakeSerialized, resultSerialized);
        }

        #endregion


        #region GetOneById

        [Fact]
        public async Task GetOneById_Returns_ServiceRequest_If_Exists()
        {
            var fakeServiceRequestDTO = new ServiceRequestDTO
            {
                Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                BuildingCode = "Fake Building Code",
                CreatedBy = "Fake User",
                CreatedDate = new DateTime(2020, 1, 1),
                LastModifiedBy = "Fake User",
                LastModifiedDate = new DateTime(2020, 2, 1),
                CurrentStatus = Enums.CurrentStatus.Complete,
                Description = "Fake Description"
            };

            var fakeServiceRequest = new ServiceRequest
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                    BuildingCode = "Fake Building Code",
                    CreatedBy = "Fake User",
                    CreatedDate = new DateTime(2020, 1, 1),
                    LastModifiedBy = "Fake User",
                    LastModifiedDate = new DateTime(2020, 2, 1),
                    CurrentStatus = "Complete",
                    Description = "Fake Description"
            };

            _serviceRequestRepositoryMock.Setup(m => m.GetOneById(It.IsAny<Guid>())).Returns(Task.FromResult(fakeServiceRequest));

            var service = new ServiceRequestService(_serviceRequestRepositoryMock.Object, _mapper);

            var result = await service.GetOneById(new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"));

            Assert.NotNull(result);

            string fakeSerialized = JsonSerializer.Serialize(fakeServiceRequestDTO);
            string resultSerialized = JsonSerializer.Serialize(result);

            Assert.Equal(fakeSerialized, resultSerialized);
        }

        #endregion
    }
}
