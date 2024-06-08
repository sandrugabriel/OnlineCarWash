using OnlineCarWash.Customers.Models;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Services.Dto;

namespace OnlineCarWash.Appointments.Dto
{
    public class AppointmentResponse
    {
        public string CustomerName { get; set; }

        public ServiceResponse Service { get; set; }

        public OptionResponse Option { get; set; }

        public DateTime ReservationDate { get; set; }

        public int TotalAmount { get; set; }
    }
}
