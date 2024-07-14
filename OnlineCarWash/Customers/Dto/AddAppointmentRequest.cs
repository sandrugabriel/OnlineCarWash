namespace OnlineCarWash.Customers.Dto;

public class AddAppointmentRequest
{
    public string nameService { get; set; }
    public string nameOption { get; set; }
    public int day { get; set; }
    public int hour { get; set; }
}