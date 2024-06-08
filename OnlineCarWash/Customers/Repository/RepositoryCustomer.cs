using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Models;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Data;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Models;

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
            var customers = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).ToListAsync();
            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public async Task<CustomerResponse> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.Include(s=>s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(c=>c.Id == id);
            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<Customer> GetById(int id)
        {
            var customer = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task<CustomerResponse> GetByNameAsync(string name)
        {
            var customer = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(c => c.Name.Equals(name));
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
            var customer = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(s => s.Id == id);
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
            var customer = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(s=>s.Id == id);

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

        public async Task<CustomerResponse> AddAppointment(int id, Service service, Option op, DateTime clientDateTime)
        {
            var customer = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(s => s.Id == id);

            Appointment appointment = new Appointment();
            appointment.OptionId = op.Id;
            appointment.Option = op;
            appointment.Customer = customer;
            appointment.CustomerId = id;
            appointment.Service = service;
            appointment.ServiceId = service.Id;
            appointment.ReservationDate = clientDateTime;
            appointment.TotalAmount = service.Price + op.Price;

            var appointmentResponse = _mapper.Map<AppointmentResponse>(appointment);

            // appointmentResponse.Service = _mapper.Map
            customer.Appointments.Add(appointment);

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            var customerResponse = _mapper.Map<CustomerResponse>(customer);
            
           // customerResponse.Appointments = 

            return customerResponse;
        }

        public async Task<CustomerResponse> DeleteAppointment(int id, Appointment appointment)
        {
            var customer = await _context.Customers.Include(s => s.Appointments).ThenInclude(s => s.Option).Include(s => s.Appointments).ThenInclude(s => s.Service).FirstOrDefaultAsync(s => s.Id == id);

            customer.Appointments.Remove(appointment);

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponse>(customer);
        }

    }
}
