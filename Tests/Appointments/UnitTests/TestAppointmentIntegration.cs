using Newtonsoft.Json;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Services.Dto;
using Test.Car.Infrastucture;
using Tests.Appointments.Helpers;
using Tests.Customers.Helpers;

namespace Tests.Appointments.UnitTests;

public class TestAppointmentIntegration : IClassFixture<ApiWebApplicationFactory>
{

    private readonly HttpClient _client;

    public TestAppointmentIntegration(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAppointments_AppointmentsFound_ReturnsOkStatusCode()
    {
        var createAppointmentRequest = TestAppointmentFactory.CreateAppointment(1);
        var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");
        await _client.GetAsync("/api/v1/ControllerAppointment/all");

        var response = await _client.GetAsync("/api/v1/ControllerAppointment/all");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllAppointments_AppointmentsFound_ReturnsNotFoundStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/ControllerAppointment/all");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAppointmentById_AppointmentFound_ReturnsOkStatusCode()
    {
        var createAppointmentRequest = TestAppointmentFactory.CreateAppointment(1);
        var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");

        
        var createCustomer = new CreateCustomerRequest {Username = "test123",Name = "test",Email = "asda@gma.com",Password = "aASda123@",PhoneNumber = "0777777777"};
        var contentCustomer = new StringContent(JsonConvert.SerializeObject(createCustomer), Encoding.UTF8, "application/json");
        var responseCustomer = await _client.PostAsync("/api/v1/ControllerCustomer/CreateCustomer",contentCustomer);
        var responseCustomerString = await responseCustomer.Content.ReadAsStringAsync();
        var resultCustomer = JsonConvert.DeserializeObject<CustomerResponse>(responseCustomerString);
        string token = resultCustomer.Token;

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        
        var createOption = new CreateOptionRequest() { Name = "cleaning inside",Price = 30};
        var contentOption = new StringContent(JsonConvert.SerializeObject(createOption), Encoding.UTF8, "application/json");
        var responseOption = await _client.PostAsync("/api/v1/ControllerOption/CreateOption",contentOption);
        var responseOptionString = await responseOption.Content.ReadAsStringAsync();
        var resultOption = JsonConvert.DeserializeObject<OptionResponse>(responseOptionString);
        
        var createService = new CreateServiceRequest { Name = "standard",Descriptions = "asdasadom",Type = "ASd",Price = 100};
        var contentService = new StringContent(JsonConvert.SerializeObject(createService), Encoding.UTF8, "application/json");
        var responseService = await _client.PostAsync("/api/v1/ControllerService/CreateService",contentService);
        var responseServiceString = await responseService.Content.ReadAsStringAsync();
        var resultService = JsonConvert.DeserializeObject<ServiceResponse>(responseServiceString);

        string name = resultOption.Name;
        var contentAddOption = new StringContent(JsonConvert.SerializeObject(name), Encoding.UTF8, "application/json");
        var responseAddOption = await _client.PutAsync($"/api/v1/ControllerService/AddOption?id={resultService.Id}",contentAddOption);
        var respinseAddOptionString = await responseAddOption.Content.ReadAsStringAsync();
        var resultAddOption = JsonConvert.DeserializeObject<ServiceResponse>(respinseAddOptionString);

        var addappointment = new AddAppointmentRequest
            { nameService = resultService.Name, nameOption = resultOption.Name, day = 2, hour = 14 };
        var contentAppointment = new StringContent(JsonConvert.SerializeObject(addappointment), Encoding.UTF8, "application/json");
        var response=  await _client.PutAsync($"/api/v1/ControllerCustomer/AddAppointment?id={resultCustomer.Id}",contentAppointment);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<CustomerResponse>(responseString);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(result.Appointments[0].Service.Name,addappointment.nameService);
        Assert.Equal(result.Appointments[0].Option.Name,addappointment.nameOption);

    }

    [Fact]
    public async Task GetAppointmentById_AppointmentNotFound_ReturnsNotFoundStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/ControllerAppointment/delete/1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}