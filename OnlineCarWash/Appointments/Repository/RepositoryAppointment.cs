using AutoMapper;
using OnlineCarWash.Appointments.Repository.interfaces;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace OnlineCarWash.Appointments.Repository
{
    public class RepositoryAppointment : IRepositoryAppointment
    {
        AppDbContext _context;
        IMapper _mapper;

        public RepositoryAppointment(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AppointmentResponse>> GetAllAsync()
        {
            var appointments = await _context.Appointments.Include(s => s.Customer).Include(s => s.Option).Include(s => s.Service).ToListAsync();
            return _mapper.Map<List<AppointmentResponse>>(appointments);
        }

        public async Task<AppointmentResponse> GetByIdAsync(int id)
        {
            var appointment = await _context.Appointments.Include(s=>s.Customer).Include(s => s.Option).Include(s=>s.Service).FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<AppointmentResponse>(appointment);
        }

        public async Task<List<DateTime>> GetAvailableTimesAsync()
        {
            DateTime now = DateTime.Now;
            DateTime start = new DateTime(now.Year, now.Month, now.Day +1, 0, 0, 0);
            DateTime end = start.AddDays(7);
            var appointments = await _context.Appointments
            .Where(a => a.ReservationDate >= start && a.ReservationDate < end)
                .ToListAsync();

            List<DateTime> availableTimes = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                DateTime day = start.AddDays(i);

                availableTimes.Add(day.AddHours(9));
                availableTimes.Add(day.AddHours(10));
                availableTimes.Add(day.AddHours(11));
                availableTimes.Add(day.AddHours(12));
                availableTimes.Add(day.AddHours(13));
                availableTimes.Add(day.AddHours(14));
                availableTimes.Add(day.AddHours(15));
                availableTimes.Add(day.AddHours(16));
                availableTimes.Add(day.AddHours(17));

            }

            foreach (var appointment in appointments)
            {
                availableTimes.RemoveAll(time => time == appointment.ReservationDate && time > DateTime.Now);
            }

            return availableTimes;
        }


    }
}
