using OnlineCarWash.Customers.Models;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineCarWash.Appointments.Dto
{
    public class CreateAppointmentRequest
    {
        public int CustomerId { get; set; }

        public int ServiceId { get; set; }

        public int OptionId { get; set; }

        public DateTime ReservationDate { get; set; }

        public int TotalAmount { get; set; }

    }
}
