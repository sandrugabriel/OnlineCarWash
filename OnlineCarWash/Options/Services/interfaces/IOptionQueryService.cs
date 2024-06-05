using OnlineCarWash.Options.Models;

namespace OnlineCarWash.Options.Services.interfaces
{
    public interface IOptionQueryService
    {
        Task<List<Option>> GetAllOption();

        Task<Option> GetByIdOption(int id);

        Task<Option> GetByNameOption(string name);

    }
}
