using Newtonsoft.Json;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Car.Infrastucture;
using Tests.Services.Helpers;

namespace Tests.Services.UnitTests
{
    public class TestServiceIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestServiceIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllServices_ServicesFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createServiceRequest = TestServiceFactory.CreateService(1);
            var content = new StringContent(JsonConvert.SerializeObject(createServiceRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerService/createService", content);

            var response = await _client.GetAsync("/api/v1/ControllerService/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetServiceById_ServiceFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createServiceRequest = TestServiceFactory.CreateService(2);
            var content = new StringContent(JsonConvert.SerializeObject(createServiceRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Service/create", content);

            var response = await _client.GetAsync("/api/v1/ControllerService/findById/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetServiceById_ServiceNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerService/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerService/createService";
            var ControllerService = new CreateServiceRequest { Name = "New Service 1", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            var content = new StringContent(JsonConvert.SerializeObject(ControllerService), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Service>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerService.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerService/createService";
            var createService = new CreateServiceRequest { Name = "New Service 1", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            var content = new StringContent(JsonConvert.SerializeObject(createService), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Service>(responseString)!;

            request = "/api/v1/ControllerService/updateService";
            var updateService = new UpdateServiceRequest { Name = "New Service 3", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            content = new StringContent(JsonConvert.SerializeObject(updateService), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Service>(responseString)!;

            Assert.Equal(updateService.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidServiceName_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerService/createService";
            var createService = new CreateServiceRequest { Name = "test", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            var content = new StringContent(JsonConvert.SerializeObject(createService), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse>(responseString)!;

            request = "/api/v1/ControllerService/updateService";
            var updateService = new UpdateServiceRequest { Name = "", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            content = new StringContent(JsonConvert.SerializeObject(updateService), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_Update_ServiceDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerService/updateService";
            var updateService = new UpdateServiceRequest { Name = "New Service 3", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            var content = new StringContent(JsonConvert.SerializeObject(updateService), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_ServiceExists_ReturnsDeletedService()
        {
            var request = "/api/v1/ControllerService/createService";
            var createService = new CreateServiceRequest { Name = "New Service 1", Descriptions = "asdsdf", Price = 100, Type = "sad" };
            var content = new StringContent(JsonConvert.SerializeObject(createService), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Service>(responseString)!;

            request = $"/api/v1/ControllerService/deleteService/{result.Id}";

            response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_ServiceDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerService/deleteService/77777";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
