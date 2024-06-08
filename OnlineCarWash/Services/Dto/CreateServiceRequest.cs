using System.ComponentModel.DataAnnotations;

namespace OnlineCarWash.Services.Dto
{
    public class CreateServiceRequest
    {
        public string Name { get; set; }

        public string Descriptions { get; set; }

        public int Price { get; set; }

        public string Type { get; set; }

    }
}
