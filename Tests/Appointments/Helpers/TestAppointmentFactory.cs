using OnlineCarWash.Appointments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Appointments.Helpers
{
    public class TestAppointmentFactory
    {
        public static AppointmentResponse CreateAppointment(int id)
        {
            return new AppointmentResponse
            {
                TotalAmount = id,
                CustomerName = "test" + id,
                ReservationDate = DateTime.Now
            };
        }

        public static List<AppointmentResponse> CreateAppointments(int count)
        {

            List<AppointmentResponse> responses = new List<AppointmentResponse>();

            for (int i = 0; i < count; i++)
            {
                responses.Add(CreateAppointment(i));
            }

            return responses;
        }
    }
}
