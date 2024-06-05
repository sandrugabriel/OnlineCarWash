using AutoMapper;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Models;

namespace OnlineCarWash.Customers.ProfileMappings
{
    public class ProfileMapping : Profile
    {

        public ProfileMapping() {

            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer,CustomerResponse>();

        }


    }
}
