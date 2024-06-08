using OnlineCarWash.Appointments.Dto;

namespace OnlineCarWash.Appointments.Repository.interfaces
{
    public interface IRepositoryAppointment
    {
        Task<List<AppointmentResponse>> GetAllAsync();

        Task<AppointmentResponse> GetByIdAsync(int id);

        Task<List<DateTime>> GetAvailableTimesAsync();

    }
}
