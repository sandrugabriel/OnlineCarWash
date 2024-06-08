using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Customers.Controllers.intefaces;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Customers.Services;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.System.Exceptions;

namespace OnlineCarWash.Customers.Controllers
{
    public class ControllerCustomer : ControllerAPICustomer
    {
        IQueryServiceCustomer _query;
        ICommandServiceCustomer _command;

        public ControllerCustomer(IQueryServiceCustomer query, ICommandServiceCustomer command)
        {
            _query = query;
            _command = command;
        }

        public override async Task<ActionResult<List<CustomerResponse>>> GetAll()
        {
            try
            {
                var customers = await _query.GetAllAsync();
                return Ok(customers);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var customers = await _query.GetByIdAsync(id);
                return Ok(customers);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> GetByName([FromQuery] string name)
        {
            try
            {
                var customers = await _query.GetByNameAsync(name);
                return Ok(customers);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> CreateCustomer([FromBody] CreateCustomerRequest createRequestCustomer)
        {
            try
            {
                var customer = await _command.CreateCustomer(createRequestCustomer);
                return Ok(customer);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> UpdateCustomer([FromQuery] int id, [FromBody] UpdateCustomerRequest updateRequest)
        {
            try
            {
                var customer = await _command.UpdateCustomer(id, updateRequest);
                return Ok(customer);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public override async Task<ActionResult<CustomerResponse>> DeleteCustomer([FromQuery] int id)
        {
            try
            {
                var customer = await _command.DeleteCustomer(id);
                return Ok(customer);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> AddAppointment([FromQuery] int id, [FromQuery] string nameService, [FromQuery] string nameOption, [FromQuery] int day, [FromQuery] int hour)
        {
            try
            {
                var customer = await _command.AddAppointment(id,nameService,nameOption,day,hour);
                return Ok(customer);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnavailableTime ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<CustomerResponse>> DeleteAppointment([FromQuery] int id, [FromQuery] string nameService, [FromQuery] string nameOption)
        {
            try
            {
                var customer = await _command.DeleteAppointment(id, nameService, nameOption);
                return Ok(customer);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
