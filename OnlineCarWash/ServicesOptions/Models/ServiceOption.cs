using OnlineCarWash.Options.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineCarWash.ServicesOptions.Models
{
    public class ServiceOption
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("OptionId")]
        public int OptionId { get; set; }

        [JsonIgnore]
        public virtual Option Option { get; set; }

        [Required]
        public int ServiceId { get; set; }

    }
}
