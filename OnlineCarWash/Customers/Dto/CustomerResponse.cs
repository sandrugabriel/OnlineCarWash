using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Models;

namespace OnlineCarWash.Customers.Dto
{
    public class CustomerResponse
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public List<AppointmentResponse> Appointments { get; set; }

    }
}
