using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Customers.Services
{
    public class CommandServiceCustomer : ICommandServiceCustomer
    {
        IRepositoryCustomer _repo;

        public CommandServiceCustomer(IRepositoryCustomer repo)
        {
            _repo = repo;
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {
            if (createRequest.Name.Equals("") || createRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            var customer = await _repo.CreateCustomer(createRequest);

            return customer;
        }

        public async Task<CustomerResponse> UpdateCustomer(int id, UpdateCustomerRequest updateRequest)
        {

            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            if (updateRequest.Name.Equals("") || updateRequest.Name.Equals("string"))
            {
                throw new InvalidName(Constants.InvalidName);
            }

            customer = await _repo.UpdateCustomer(id, updateRequest);
            return customer;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }
            await _repo.DeleteCustomer(id);

            return customer;
        }
    }
}
