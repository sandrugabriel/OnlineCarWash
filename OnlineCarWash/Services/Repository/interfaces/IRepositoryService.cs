using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Dto;

namespace OnlineCarWash.Services.Repository.interfaces
{
    public interface IRepositoryService
    {
        Task<List<ServiceResponse>> GetAllAsync();

        Task<ServiceResponse> GetByIdAsync(int id);

        Task<ServiceResponse> GetByNameAsync(string name);

        Task<ServiceResponse> CreateService(CreateServiceRequest createRequest);

        Task<ServiceResponse> UpdateService(int id, UpdateServiceRequest updateRequest);

        Task<ServiceResponse> DeleteService(int id);

        Task<ServiceResponse> AddOption(int id, Option option);

        Task<ServiceResponse> DeleteOption(int id, Option option);


    }
}
