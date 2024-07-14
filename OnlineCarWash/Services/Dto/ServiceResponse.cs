using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.ServicesOptions.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineCarWash.Services.Dto
{
    public class ServiceResponse
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Descriptions { get; set; }

        public int Price { get; set; }

        public string Type { get; set; }

        public List<OptionResponse> Options { get; set; }
    }
}
