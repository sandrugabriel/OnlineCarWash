using AutoMapper;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Models;
using OnlineCarWash.ServicesOptions.Models;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace OnlineCarWash.Services.ProfileMappings
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<CreateServiceRequest, Service>();
            CreateMap<Service, ServiceResponse>();
            CreateMap<ServiceOption, OptionResponse>().ForPath(s => s.Name, op => op.MapFrom(s => s.Option.Name))
                .ForPath(s => s.Price, op => op.MapFrom(s => s.Option.Price));
           CreateMap<Option, OptionResponse>();
        }
    }
}
