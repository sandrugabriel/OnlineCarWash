using OnlineCarWash.ServicesOptions.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCarWash.Services.Models
{
    public class Service
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        public string Name { get; set; }

        [Required]
        public string Descriptions { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string Type { get; set; }


        public virtual List<ServiceOption> Options { get; set; }

    }
}
