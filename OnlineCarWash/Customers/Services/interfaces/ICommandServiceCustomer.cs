using OnlineCarWash.Customers.Dto;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Customers.Services.interfaces
{
    public interface ICommandServiceCustomer
    {
        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

    }
}
