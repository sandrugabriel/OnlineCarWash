using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Options.Services
{
    public class OptionQueryService : IOptionQueryService
    {
        IRepositoryOption _repo;

        public OptionQueryService(IRepositoryOption repo)
        {
            _repo = repo;
        }

        public async Task<List<Option>> GetAllOption()
        {
           var options = await _repo.GetAllOption();
            if(options.Count == 0)  return new List<Option>();

            return options;
        }

        public async Task<Option> GetByIdOption(int id)
        {
           var option = await _repo.GetByIdOption(id);
            if(option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return option;
        }

        public async Task<Option> GetByNameOption(string name)
        {
            var option = await _repo.GetByNameOption(name);
            if (option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return option;
        }
    }
}
