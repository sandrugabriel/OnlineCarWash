using System.ComponentModel.DataAnnotations;

namespace OnlineCarWash.Options.Dto
{
    public class CreateOptionRequest
    {
        public string Name { get; set; }

        public int Price { get; set; }
    }
}
