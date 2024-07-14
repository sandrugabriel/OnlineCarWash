using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.System.Constatns;
using OnlineCarWash.System.Exceptions;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace OnlineCarWash.Options.Services
{
    public class OptionCommandService : IOptionCommandService
    {

        IRepositoryOption _repo;

        public OptionCommandService(IRepositoryOption repo)
        {
            _repo = repo;
        }

        public async Task<Option> CreateOption(CreateOptionRequest createOptionRequest)
        {
            if (createOptionRequest.Price <= 0) throw new InvalidPrice(Constants.InvalidPrice);

            if(createOptionRequest.Name.Length <= 1) throw new InvalidName(Constants.InvalidName);

            var option = await _repo.CreateOption(createOptionRequest);

            return option;
        }

        public async Task<Option> DeleteOption(int id)
        {
            var option = await _repo.GetByIdOption(id);
            if (option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            await _repo.DeleteOption(id);

            return option;
        }

        public async Task<Option> UpdateOption(int id, UpdateOptionRequest updateOptionRequest)
        {
            var option = await _repo.GetByIdOption(id);
            if (option == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);
            
            if (updateOptionRequest.Name.Length <= 1) throw new InvalidName(Constants.InvalidName);
            
            option = await _repo.UpdateOption(id, updateOptionRequest);

            return option;
        }
    
    }
}
