using OnlineCarWash.Appointments.Models;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Models;

namespace OnlineCarWash.Customers.Repository.interfaces
{
    public interface IRepositoryCustomer
    {

        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<Customer> GetById(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddAppointment(int id, Service service, Option op, DateTime clientDateTime);

        Task<CustomerResponse> DeleteAppointment(int id, Appointment appointment);

    }
}
