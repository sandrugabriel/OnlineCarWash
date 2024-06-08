using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Appointments.Controllers.interfaces;
using OnlineCarWash.Appointments.Dto;
using OnlineCarWash.Appointments.Services.interfaces;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Appointments.Controllers
{
    public class ControllerAppointment : ControllerAPIAppointment
    {

        IAppointmentQueryService _query;

        public ControllerAppointment(IAppointmentQueryService query)
        {
            _query = query;
        }

        public override async Task<ActionResult<List<AppointmentResponse>>> GetAll()
        {
            try
            {
                var appointments = await _query.GetAllAsync();
                return Ok(appointments);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<AppointmentResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var appointments = await _query.GetByIdAsync(id);
                return Ok(appointments);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
