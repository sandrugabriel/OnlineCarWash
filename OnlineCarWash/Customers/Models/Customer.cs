using Microsoft.AspNetCore.Identity;
using OnlineCarWash.Appointments.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCarWash.Customers.Models
{
    public class Customer : IdentityUser<int>
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Appointment> Appointments { get; set; }

    }
}
