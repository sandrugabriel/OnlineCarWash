using OnlineCarWash.Customers.Dto;

namespace OnlineCarWash.Customers.Services.interfaces
{
    public interface IQueryServiceCustomer
    {
        Task<List<CustomerResponse>> GetAllAsync();

        Task<CustomerResponse> GetByIdAsync(int id);

        Task<CustomerResponse> GetByNameAsync(string name);
    }
}
