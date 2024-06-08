using OnlineCarWash.Customers.Models;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Services.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineCarWash.Appointments.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceId { get; set; }

        [JsonIgnore]
        public virtual Service Service { get; set; }

        [ForeignKey("OptionId")]
        public int OptionId { get; set; }

        [JsonIgnore]
        public virtual Option Option { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public int TotalAmount { get; set; }


    }
}
