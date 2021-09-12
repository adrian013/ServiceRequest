using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ServiceRequestManager.Application.Services;
using ServiceRequestManager.Application.DTO;
using ServiceRequestManager.Api.Controllers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ServiceRequestManager.Application.Exceptions;

namespace ServiceRequestManager.API.Test.Controllers
{
    public class ServiceRequestControllerTest
    {
        private readonly Mock<IServiceRequestService> _serviceRequestServiceMock;

        public ServiceRequestControllerTest()
        {
            _serviceRequestServiceMock = new Mock<IServiceRequestService>();
        }

        #region GetAll
        [Fact]
        public void GetAll_Returns_200_If_ServiceRequests_Are_Found()
        {
            var output = new List<ServiceRequestDTO>
            {
                new ServiceRequestDTO
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"),
                    BuildingCode = "Fake Building Code",
                    CreatedBy = "Fake User",
                    CreatedDate = new DateTime(2020,1,1),
                    LastModifiedBy = "Fake User",
                    LastModifiedDate = new DateTime(2020,2,1),
                    CurrentStatus = Enums.CurrentStatus.Complete,
                    Description = "Fake Description"
                },
                new ServiceRequestDTO
                {
                    Id = new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"),
                    BuildingCode = "Fake Building Code 2",
                    CreatedBy = "Fake User 2",
                    CreatedDate = new DateTime(2020,1,1),
                    LastModifiedBy = "Fake User 2",
                    LastModifiedDate = new DateTime(2020,2,1),
                    CurrentStatus = Enums.CurrentStatus.Complete,
                    Description = "Fake Description 2"
                }
            };
            _serviceRequestServiceMock.Setup(m => m.GetAll()).Returns(output);

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);

            var result = controller.GetAll();

            string outputSerialized = JsonSerializer.Serialize(output);
            string resultSerialized = JsonSerializer.Serialize(((OkObjectResult)result.Result).Value);

            Assert.Equal(outputSerialized, resultSerialized);
        }

        [Fact]
        public void GetAll_Returns_204_If_No_ServiceRequests_Are_Found()
        {

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);

            var result = controller.GetAll();

            Assert.IsType<NoContentResult>(result.Result);
        }
        #endregion

        #region GetById
        [Fact]
        public async Task GetById_Returns_200_If_ServiceRequest_Is_Found()
        {
            var output = new ServiceRequestDTO
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
            _serviceRequestServiceMock.Setup(m => m.GetOneById(It.IsAny<Guid>())).Returns(Task.FromResult(output));

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);

            var result = await controller.GetById(new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"));

            string outputSerialized = JsonSerializer.Serialize(output);
            string resultSerialized = JsonSerializer.Serialize(((OkObjectResult)result.Result).Value);

            Assert.Equal(outputSerialized, resultSerialized);
        }

        [Fact]
        public async Task GetById_Returns_404_If_ServiceRequests_Is_Not_Found()
        {
            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);

            var result = await controller.GetById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }
        #endregion

        #region Post
        [Fact]
        public async Task Post_Returns_201_If_ServiceRequests_Creates_Ok()
        {
            _serviceRequestServiceMock.Setup(m => m.Create(It.IsAny<ServiceRequestPostDTO>())).Returns(Task.FromResult(Guid.NewGuid()));

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);
            var body = new ServiceRequestPostDTO()
            {
                BuildingCode = "Fake Code",
                CurrentStatus = "Created",
                Description = "Fake Description"
            };

            var result = await controller.Post(body);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Post_Returns_400_If_Bad_Body()
        {
            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);
            var body = new ServiceRequestPostDTO()
            {
                BuildingCode = "Fake Code",
                CurrentStatus = "WrongStatus",
                Description = "Fake Description"
            };

            var result = await controller.Post(body);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Put
        [Fact]
        public async Task Put_Returns_201_If_ServiceRequests_Updates_Ok()
        {
            _serviceRequestServiceMock.Setup(m => m.Create(It.IsAny<ServiceRequestPostDTO>())).Returns(Task.FromResult(Guid.NewGuid()));

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);
            var body = new ServiceRequestPostDTO()
            {
                BuildingCode = "Fake Code",
                CurrentStatus = "Created",
                Description = "Fake Description"
            };

            var result = await controller.Put(Guid.NewGuid(), body);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Put_Returns_400_If_Bad_Body()
        {
            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);
            var body = new ServiceRequestPostDTO()
            {
                BuildingCode = "Fake Code",
                CurrentStatus = "WrongStatus",
                Description = "Fake Description"
            };

            var result = await controller.Put(Guid.NewGuid(), body);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task Put_Returns_404_If_ServiceRequest_Does_Not_Exists()
        {
            _serviceRequestServiceMock.Setup(m => m.Update(It.IsAny<ServiceRequestPostDTO>(), It.IsAny<Guid>())).Throws(new NotFoundExeption());
            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);
            var body = new ServiceRequestPostDTO()
            {
                BuildingCode = "Fake Code",
                CurrentStatus = "Created",
                Description = "Fake Description"
            };

            var result = await controller.Put(Guid.NewGuid(), body);

            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #region GetById
        [Fact]
        public async Task Delete_Returns_201_If_ServiceRequest_Is_Deleted()
        {
            _serviceRequestServiceMock.Setup(m => m.Delete(It.IsAny<Guid>())).Returns(Task.FromResult(true));

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);

            var result = await controller.Delete(new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"));
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task Delete_Returns_404_If_ServiceRequests_Is_Not_Found()
        {
            _serviceRequestServiceMock.Setup(m => m.Delete(It.IsAny<Guid>())).Throws(new NotFoundExeption());

            var controller = new ServiceRequestController(_serviceRequestServiceMock.Object);

            var result = await controller.Delete(new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"));
            
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion
    }
}
