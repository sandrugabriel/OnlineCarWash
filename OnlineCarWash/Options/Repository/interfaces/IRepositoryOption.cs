using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;

namespace OnlineCarWash.Options.Repository.interfaces
{
    public interface IRepositoryOption
    {

        Task<List<Option>> GetAllOption();

        Task<Option> GetByIdOption(int id);

        Task<Option> GetByNameOption(string name);

        Task<Option> CreateOption(CreateOptionRequest createOptionRequest);

        Task<Option> UpdateOption(int id,UpdateOptionRequest updateOptionRequest);

        Task<Option> DeleteOption(int id);
    }
}
