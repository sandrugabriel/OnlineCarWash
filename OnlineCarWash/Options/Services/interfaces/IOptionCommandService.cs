using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;

namespace OnlineCarWash.Options.Services.interfaces
{
    public interface IOptionCommandService
    {
        Task<Option> CreateOption(CreateOptionRequest createOptionRequest);

        Task<Option> UpdateOption(int id, UpdateOptionRequest updateOptionRequest);

        Task<Option> DeleteOption(int id);

    }
}
