using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Repository.interfaces;
using OnlineCarWash.Services.ServiceCommandQuery.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;
using System.Xml.Linq;

namespace OnlineCarWash.Services.ServiceCommandQuery
{
    public class ServiceCommandService : IServiceCommandService
    {
        IRepositoryService _repo;
        IRepositoryOption _repoOption;

        public ServiceCommandService(IRepositoryService repo, IRepositoryOption option)
        {
            _repo = repo;
            _repoOption = option;
        }

        public async Task<ServiceResponse> AddOption(int id, string name)
        {
            var service = await _repo.GetByIdAsync(id);
            if (service == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var option = await _repoOption.GetByNameOption(name);
            if(option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            if (service.Options.FirstOrDefault(s => s.Name == option.Name && s.Price == option.Price) != null)
                throw new AlreadyExistOption(Constants.AlreadyOption);

            service = await _repo.AddOption(id,option);

            return service;
        }

        public async Task<ServiceResponse> CreateService(CreateServiceRequest createRequest)
        {
            if(createRequest.Price <= 0) throw new InvalidPrice(Constants.InvalidPrice);

            return await _repo.CreateService(createRequest);
        }

        public async Task<ServiceResponse> DeleteOption(int id, string name)
        {
            var service = await _repo.GetByIdAsync(id);
            if (service == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            var option = await _repoOption.GetByNameOption(name);
            if (option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            if (service.Options.FirstOrDefault(s => s.Name == option.Name && s.Price == option.Price) == null)
                throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repo.DeleteOption(id, option);

            return service;
        }

        public async Task<ServiceResponse> DeleteService(int id)
        {
            var service = await _repo.GetByIdAsync(id);
            if (service == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repo.DeleteService(id);

            return service;
        }

        public async Task<ServiceResponse> UpdateService(int id, UpdateServiceRequest updateRequest)
        {
            var service = await _repo.GetByIdAsync(id);
            if (service == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            service = await _repo.UpdateService(id, updateRequest);

            return service;
        }
    }
}
