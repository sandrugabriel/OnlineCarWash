using Newtonsoft.Json;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.Car.Infrastucture;
using Tests.Customers.Helpers;

namespace Tests.Customers.UnitTests
{
    public class TestCustomerIntegration : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public TestCustomerIntegration(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCustomers_CustomersFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createCustomerRequest = TestCustomerFactory.CreateCustomer(1);
            var content = new StringContent(JsonConvert.SerializeObject(createCustomerRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerCustomer/createCustomer", content);

            var response = await _client.GetAsync("/api/v1/ControllerCustomer/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetCustomerById_CustomerFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createCustomerRequest = TestCustomerFactory.CreateCustomer(2);
            var content = new StringContent(JsonConvert.SerializeObject(createCustomerRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/Customer/create", content);

            var response = await _client.GetAsync("/api/v1/ControllerCustomer/findById/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetCustomerById_CustomerNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerCustomer/findById/9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerCustomer/createCustomer";
            var ControllerCustomer = new CreateCustomerRequest { Name = "New Customer 1", Email = "asd@gm.con",Password ="Asd",PhoneNumber ="077777" };
            var content = new StringContent(JsonConvert.SerializeObject(ControllerCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Customer>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerCustomer.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerCustomer/createCustomer";
            var createCustomer = new CreateCustomerRequest { Name = "New Customer 1", Email = "asd@gm.con", Password = "Asd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Customer>(responseString)!;

            request = "/api/v1/ControllerCustomer/updateCustomer";
            var updateCustomer = new UpdateCustomerRequest { Name = "New Customer 3", Email = "asd@gm.con" };
            content = new StringContent(JsonConvert.SerializeObject(updateCustomer), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);

            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Customer>(responseString)!;

            Assert.Equal(updateCustomer.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidCustomerName_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerCustomer/createCustomer";
            var createCustomer = new CreateCustomerRequest { Name = "test", Email = "asd@gm.con", Password = "Asd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            request = "/api/v1/ControllerCustomer/updateCustomer";
            var updateCustomer = new UpdateCustomerRequest { Name = "", Email = "asd@gm.con",};
            content = new StringContent(JsonConvert.SerializeObject(updateCustomer), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Put_Update_CustomerDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerCustomer/updateCustomer";
            var updateCustomer = new UpdateCustomerRequest { Name = "New Customer 3", Email = "asd@gm.con" };
            var content = new StringContent(JsonConvert.SerializeObject(updateCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_CustomerExists_ReturnsDeletedCustomer()
        {
            var request = "/api/v1/ControllerCustomer/createCustomer";
            var createCustomer = new CreateCustomerRequest { Name = "New Customer 1", Email = "asd@gm.con", Password = "Asd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Customer>(responseString)!;

            request = $"/api/v1/ControllerCustomer/deleteCustomer/{result.Id}";

            response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_CustomerDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerCustomer/deleteCustomer/77777";

            var response = await _client.DeleteAsync(request);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
