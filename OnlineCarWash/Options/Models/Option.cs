using Microsoft.AspNetCore.Hosting.Server;
using OnlineCarWash.ServicesOptions.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCarWash.Options.Models
{
    public class Option
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public virtual List<ServiceOption> Services { get; set; }

    }
}
