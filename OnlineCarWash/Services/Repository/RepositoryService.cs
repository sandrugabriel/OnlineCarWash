using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Data;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Models;
using OnlineCarWash.Services.Repository.interfaces;
using OnlineCarWash.ServicesOptions.Models;

namespace OnlineCarWash.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> AddOption(int id, Option option)
        {

            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);

            ServiceOption serviceOption = new ServiceOption();
            serviceOption.Option = option;
            serviceOption.OptionId = option.Id;
            serviceOption.ServiceId = id;
            serviceOption.Service = service;

            service.Options.Add(serviceOption);

            _context.Services.Update(service);
            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse> CreateService(CreateServiceRequest createRequest)
        {

            var service = _mapper.Map<Service>(createRequest);

            _context.Services.Add(service);

            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceResponse>(service);

        }

        public async Task<ServiceResponse> DeleteOption(int id, Option option)
        {
            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);

            service.Options.Remove(service.Options.FirstOrDefault(s=>s.Option == option));
            _context.Services.Update(service);

            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse> DeleteService(int id)
        {
            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);

            _context.Services.Remove(service);

            await _context.SaveChangesAsync();

            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<List<ServiceResponse>> GetAllAsync()
        {
            List<Service> services = await _context.Services.Include(s => s.Options).ThenInclude(s=>s.Option).ToListAsync();

            return _mapper.Map<List<ServiceResponse>>(services);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {

            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Name == name);

            return _mapper.Map<ServiceResponse>(service);
        }

        public async Task<Service> GetById(int id)
        {

            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);

            return service;
        }

        public async Task<Service> GetByName(string name)
        {
            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Name == name);

            return service;
        }


        public async Task<ServiceResponse> UpdateService(int id, UpdateServiceRequest updateRequest)
        {
            var service = await _context.Services.Include(s => s.Options).
                ThenInclude(s => s.Option).FirstOrDefaultAsync(s => s.Id == id);

            service.Name = updateRequest.Name ?? service.Name;
            service.Price = updateRequest.Price ?? service.Price;

            _context.Services.Update(service);

            await _context.SaveChangesAsync();


            return _mapper.Map<ServiceResponse>(service);
        }

    }
}
