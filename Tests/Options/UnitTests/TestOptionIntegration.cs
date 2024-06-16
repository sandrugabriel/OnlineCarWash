using Newtonsoft.Json;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Car.Infrastucture;
using Tests.Options.Helpers;

namespace Tests.Options.UnitTests
{
    public class TestOptionIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestOptionIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllOptions_OptionsFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createOptionRequest = TestOptionFactory.CreateOption(1);
            var content = new StringContent(JsonConvert.SerializeObject(createOptionRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerOption/createOption", content);

            var response = await _client.GetAsync("/api/v1/ControllerOption/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOptionById_OptionFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createOptionRequest = TestOptionFactory.CreateOption(2);
            var content = new StringContent(JsonConvert.SerializeObject(createOptionRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Option/create", content);

            var response = await _client.GetAsync("/api/v1/ControllerOption/findById/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOptionById_OptionNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerOption/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerOption/createOption";
            var ControllerOption = new CreateOptionRequest { Name = "New Option 1", Price = 10 };
            var content = new StringContent(JsonConvert.SerializeObject(ControllerOption), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerOption.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerOption/createOption";
            var createOption = new CreateOptionRequest { Name = "New Option 1", Price = 10 };
            var content = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString)!;

            request = "/api/v1/ControllerOption/updateOption";
            var updateOption = new UpdateOptionRequest { Name = "New Option 3", Price = 10 };
            content = new StringContent(JsonConvert.SerializeObject(updateOption), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Option>(responseString)!;

            Assert.Equal(updateOption.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidOptionName_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerOption/createOption";
            var createOption = new CreateOptionRequest { Name = "test", Price = 10 };
            var content = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<OptionResponse>(responseString)!;

            request = "/api/v1/ControllerOption/updateOption";
            var updateOption = new UpdateOptionRequest { Name = "", Price = 10 };
            content = new StringContent(JsonConvert.SerializeObject(updateOption), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_Update_OptionDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerOption/updateOption";
            var updateOption = new UpdateOptionRequest { Name = "New Option 3", Price = 10 };
            var content = new StringContent(JsonConvert.SerializeObject(updateOption), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_OptionExists_ReturnsDeletedOption()
        {
            var request = "/api/v1/ControllerOption/createOption";
            var createOption = new CreateOptionRequest { Name = "New Option 1", Price = 10 };
            var content = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString)!;

            request = $"/api/v1/ControllerOption/deleteOption/{result.Id}";

            response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_OptionDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerOption/deleteOption/77777";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
