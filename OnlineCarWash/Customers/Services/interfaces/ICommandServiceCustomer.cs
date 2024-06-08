using OnlineCarWash.Customers.Dto;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Customers.Services.interfaces
{
    public interface ICommandServiceCustomer
    {
        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

        Task<CustomerResponse> AddAppointment(int id,string nameService,string option,int day, int hour);

        Task<CustomerResponse> DeleteAppointment(int id,string nameService, string option);

    }
}
