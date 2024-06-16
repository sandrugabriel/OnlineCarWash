using Newtonsoft.Json;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Models;
using System.Net;
using System.Text;
using Test.Car.Infrastucture;
using Tests.Appointments.Helpers;

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
    public async Task GetAppointmentById_AppointmentFound_ReturnsOkStatusCode_ValidResponse()
    {
        var createAppointmentRequest = TestAppointmentFactory.CreateAppointment(1);
        var content = new StringContent(JsonConvert.SerializeObject(createAppointmentRequest), Encoding.UTF8, "application/json");
        await _client.PostAsync("/api/v1/ControllerAppointment/create", content);

        var response = await _client.GetAsync("api/v1/ControllerAppointment/findById/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAppointmentById_AppointmentNotFound_ReturnsNotFoundStatusCode()
    {
        var response = await _client.GetAsync("/api/v1/ControllerAppointment/1");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}