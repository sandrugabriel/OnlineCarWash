using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Models;

namespace OnlineCarWash.Customers.Repository.interfaces
{
    public interface IRepositoryCustomer
    {

        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<CustomerResponse> GetByNameAsync(string name);

        Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest);

        Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest);

        Task<CustomerResponse> DeleteCustomer(int id);

    }
}
