using System.ComponentModel.DataAnnotations;

namespace OnlineCarWash.Customers.Dto
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}
