using Newtonsoft.Json;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using OnlineCarWash.Customers.Dto;
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
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            var createOptionRequest = TestOptionFactory.CreateOption(1);
            content = new StringContent(JsonConvert.SerializeObject(createOptionRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerOption/createOption", content);

            response = await _client.GetAsync("/api/v1/ControllerOption/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOptionById_OptionFound_ReturnsOkStatusCode_ValidResponse()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            var createOptionRequest = new CreateOptionRequest{Name = "Asd",Price = 12};
             content = new StringContent(JsonConvert.SerializeObject(createOptionRequest), Encoding.UTF8, "application/json");
             response = await _client.PostAsync($"/api/v1/ControllerOption/CreateOption",content);
             responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString);

            var responseId = await _client.GetAsync($"/api/v1/ControllerOption/FindById?id={result.Id}");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(createOptionRequest.Name,result.Name);
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
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
             request = "/api/v1/ControllerOption/createOption";
            var ControllerOption = new CreateOptionRequest { Name = "New Option 1", Price = 10 };
            content = new StringContent(JsonConvert.SerializeObject(ControllerOption), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

             responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString);

            Assert.NotNull(result);
            Assert.Equal(ControllerOption.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        { 
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
            request = "/api/v1/ControllerOption/CreateOption";
            var createOption = new CreateOptionRequest { Name = "ASDaas asd", Price = 10 };
            content = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");

           response = await _client.PostAsync(request, content);
             responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString)!;

            request = $"/api/v1/ControllerOption/UpdateOption?id={result.Id}";
            var updateOption = new UpdateOptionRequest { Name = "tessdsf" };
            content =  new StringContent(JsonConvert.SerializeObject(updateOption), Encoding.UTF8, "application/json");
            response = await _client.PutAsync(request,content);
            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Option>(responseString);
            
            Assert.Equal(updateOption.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_InvalidOptionName_ReturnsBadRequestStatusCode()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
             request = "/api/v1/ControllerOption/CreateOption";
            var createOption = new CreateOptionRequest { Name = "ASDaas asd", Price = 10 };
           content = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");

            response = await _client.PostAsync(request, content);
             responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString)!;

            request = $"/api/v1/ControllerOption/UpdateOption?id={result.Id}";
            var updateOption = new UpdateOptionRequest { Name = "" };
            content =  new StringContent(JsonConvert.SerializeObject(updateOption), Encoding.UTF8, "application/json");
            response = await _client.PutAsync(request,content);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result.Name,updateOption.Name);
        }

        [Fact]
        public async Task Put_Update_OptionDoesNotExist_ReturnsNotFoundStatusCode()
        {
            
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
             request = "/api/v1/ControllerOption/updateOption";
            var updateOption = new UpdateOptionRequest { Name = "New Option 3", Price = 10 };
             content = new StringContent(JsonConvert.SerializeObject(updateOption), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_OptionExists_ReturnsDeletedOption()
        {
            var request = "/api/v1/ControllerCustomer/CreateCustomer";
            var createCustomer = new CreateCustomerRequest {Username = "test",Name = "New Customer 1", Email = "asd@gm.con", Password = "Aasd12312@sd", PhoneNumber = "077777" };
            var content = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<CustomerResponse>(responseString)!;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",result1.Token);
            
             request = "/api/v1/ControllerOption/CreateOption";
            var createOption = new CreateOptionRequest { Name = "New Option 1", Price = 10 };
           content = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");

             response = await _client.PostAsync(request, content);
            responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Option>(responseString)!;

            
            request = $"/api/v1/ControllerOption/DeleteOption?id={result.Id}";

            response = await _client.DeleteAsync(request);
            responseString = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<Option>(responseString);
            
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name,createOption.Name);
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
