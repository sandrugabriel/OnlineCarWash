using AutoMapper;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Models;

namespace OnlineCarWash.Appointments.ProfileMapping
{
    public class ProfileMappings : Profile
    {
        public ProfileMappings() {
            CreateMap<Appointment, AppointmentResponse>();
            CreateMap<CreateAppointmentRequest, Appointment>();
        }
    }
}
