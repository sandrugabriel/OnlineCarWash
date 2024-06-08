using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Dto;

namespace OnlineCarWash.Services.ServiceCommandQuery.interfaces
{
    public interface IServiceCommandService
    {
        Task<ServiceResponse> CreateService(CreateServiceRequest createRequest);

        Task<ServiceResponse> UpdateService(int id, UpdateServiceRequest updateRequest);

        Task<ServiceResponse> DeleteService(int id);

        Task<ServiceResponse> AddOption(int id, string option);

        Task<ServiceResponse> DeleteOption(int id, string option);
    }
}
