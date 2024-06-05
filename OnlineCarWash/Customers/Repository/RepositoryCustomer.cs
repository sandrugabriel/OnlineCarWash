using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Data;

namespace OnlineCarWash.Customers.Repository
{
    public class RepositoryCustomer : IRepositoryCustomer
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryCustomer(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerResponse>> GetAllAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c=>c.Id == id);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Name.Equals(name));
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> CreateCustomer(CreateCustomerRequest createRequest)
        {

            var customer = _mapper.Map<Customer>(createRequest);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }
        public async Task<CustomerResponse> UpdateCustomer(int id,  UpdateCustomerRequest updateRequest)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(s => s.Id == id);
            customer.PhoneNumber = updateRequest.PhoneNumber ?? customer.PhoneNumber;
            customer.Name = updateRequest.Name ?? customer.Name;
            customer.Email = updateRequest.Email ?? customer.Email;

            _context.Customers.Update(customer);

            await _context.SaveChangesAsync();

            CustomerResponse customerView = _mapper.Map<CustomerResponse>(customer);

            return customerView;
        }

        public async Task<CustomerResponse> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }
    }
}
