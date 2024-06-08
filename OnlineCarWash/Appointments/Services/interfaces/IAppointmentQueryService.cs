using OnlineCarWash.Appointments.Dto;

namespace OnlineCarWash.Appointments.Services.interfaces
{
    public interface IAppointmentQueryService
    {
        Task<List<AppointmentResponse>> GetAllAsync();

        Task<AppointmentResponse> GetByIdAsync(int id);
    }
}
