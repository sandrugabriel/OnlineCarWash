using OnlineCarWash.Services.Dto;

namespace OnlineCarWash.Services.ServiceCommandQuery.interfaces
{
    public interface IServiceQueryService
    {
        Task<List<ServiceResponse>> GetAllAsync();

        Task<ServiceResponse> GetByIdAsync(int id);

        Task<ServiceResponse> GetByNameAsync(string name);
    }
}
