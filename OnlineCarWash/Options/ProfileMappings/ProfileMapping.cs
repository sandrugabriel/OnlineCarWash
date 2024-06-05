using AutoMapper;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;

namespace OnlineCarWash.Options.ProfileMappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<CreateOptionRequest, Option>();
        }

    }
}
