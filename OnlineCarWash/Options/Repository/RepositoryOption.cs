using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Data;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Repository.interfaces;

namespace OnlineCarWash.Options.Repository
{
    public class RepositoryOption : IRepositoryOption
    {

        AppDbContext _context;
        IMapper _mapper;

        public RepositoryOption(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Option> CreateOption(CreateOptionRequest createOptionRequest)
        {
            var option = _mapper.Map<Option>(createOptionRequest);

            _context.Options.Add(option);

            await _context.SaveChangesAsync();

            return option;
        }

        public async Task<Option> DeleteOption(int id)
        {
           var option = await _context.Options.FirstOrDefaultAsync(s=>s.Id == id);

            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
            return option;
        }

        public async Task<List<Option>> GetAllOption()
        {
            return await _context.Options.ToListAsync();
        }

        public async Task<Option> GetByIdOption(int id)
        {
            return await _context.Options.FirstOrDefaultAsync(s=>s.Id == id);
        }

        public async Task<Option> GetByNameOption(string name)
        {
            return await _context.Options.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task<Option> UpdateOption(int id,UpdateOptionRequest updateOptionRequest)
        {
            var option = await _context.Options.FirstOrDefaultAsync(s => s.Id == id);

            option.Name = updateOptionRequest.Name ?? option.Name;
            option.Price = updateOptionRequest.Price ?? option.Price;

            _context.Options.Update(option);

            await _context.SaveChangesAsync();

            return option;
        }
    }
}
