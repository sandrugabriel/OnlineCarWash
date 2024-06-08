using FluentMigrator.Runner.Versioning;
using OnlineCarWash.Appointments.Repository.interfaces;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Services.Repository.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Customers.Services
{
    public class CommandServiceCustomer : ICommandServiceCustomer
    {
        IRepositoryCustomer _repo;
        IRepositoryService _repoService;
        IRepositoryAppointment _repoAppointment;
        IRepositoryOption _repoOptio;

        public CommandServiceCustomer(IRepositoryCustomer repo,IRepositoryService repositoryService, IRepositoryAppointment repositoryAppointment,IRepositoryOption repositoryOption)
        {
            _repo = repo;
            _repoService = repositoryService;
            _repoAppointment = repositoryAppointment;
            _repoOptio = repositoryOption;
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

        public async Task<CustomerResponse> AddAppointment(int id, string nameService, string option, int day, int hour)
        {
            var customer = await _repo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var service1 = await _repoService.GetByName(nameService);
            if (service1 == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var service = await _repoService.GetByNameAsync(nameService);

            if (service.Options.FirstOrDefault(s=>s.Name == option) == null)
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var op = await _repoOptio.GetByNameOption(option);
            DateTime now = DateTime.Now;

            DateTime clientDateTime = new DateTime(now.Year, now.Month, day, hour, 0, 0);

            var availableDate = await _repoAppointment.GetAvailableTimesAsync();
            if (!availableDate.Contains(clientDateTime))
                throw new UnavailableTime(Constants.UnavailableTime);

            customer = await _repo.AddAppointment(id,service1,op,clientDateTime);

            return customer;
        }

        public async Task<CustomerResponse> DeleteAppointment(int id, string nameService, string option)
        {
            var customer = await _repo.GetById(id);

            if (customer == null)
            {
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            }

            var appointment = customer.Appointments.FirstOrDefault(s=>s.Service.Name == nameService && s.Option.Name == option);
        
            if (appointment == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var custresponse = await _repo.GetByIdAsync(id);

           custresponse = await _repo.DeleteAppointment(id,appointment);

            return custresponse;
        }
    }
}
