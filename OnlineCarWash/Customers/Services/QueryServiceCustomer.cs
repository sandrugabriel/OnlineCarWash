using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Customers.Services
{
    public class QueryServiceCustomer : IQueryServiceCustomer
    {
        IRepositoryCustomer _repo;

        public QueryServiceCustomer(IRepositoryCustomer repo)
        {
            _repo = repo;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
           var customers = await _repo.GetAllAsync();
            if (customers.Count == 0) throw new ItemsDoNotExist(Constants.ItemsDoNotExist);

            return customers;
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if(customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _repo.GetByNameAsync(name);
            if (customer == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return customer;
        }
    }
}
