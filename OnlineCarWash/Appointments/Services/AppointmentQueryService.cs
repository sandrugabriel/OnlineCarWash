using OnlineCarWash.Appointments.Services.interfaces;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Repository.interfaces;
using OnlineCarWash.System.Exceptions;
using OnlineCarWash.System.Constatns;

namespace OnlineCarWash.Appointments.Services
{
    public class AppointmentQueryService : IAppointmentQueryService
    {
        IRepositoryAppointment _repo;

        public AppointmentQueryService(IRepositoryAppointment repo)
        {
            _repo = repo;
        }

        public async Task<List<AppointmentResponse>> GetAllAsync()
        {
            var appointments = await _repo.GetAllAsync();
            if (appointments.Count == 0) return new List<AppointmentResponse>();

            return appointments;
        }

        public async Task<AppointmentResponse> GetByIdAsync(int id)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null) throw new ItemDoesNotExist(Constants.ItemDoesNotExist);

            return appointment;
        }
    }
}
