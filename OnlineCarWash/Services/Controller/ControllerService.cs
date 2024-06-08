using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Services.Dto;
using OnlineCarWash.Services.Controller.interfaces;
using OnlineCarWash.System.Exceptions;
using OnlineCarWash.Services.ServiceCommandQuery.interfaces;
using OnlineCarWash.Services.Dto;

namespace OnlineCarWash.Services.Controller
{
    public class ControllerService : ControllerAPIService
    {
        IServiceQueryService _query;
        IServiceCommandService _command;

        public ControllerService(IServiceQueryService query, IServiceCommandService command)
        {
            _query = query;
            _command = command;
        }

        public override async Task<ActionResult<List<ServiceResponse>>> GetAll()
        {
            try
            {
                var services = await _query.GetAllAsync();
                return Ok(services);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ServiceResponse>> GetById([FromQuery] int id)
        {
            try
            {
                var services = await _query.GetByIdAsync(id);
                return Ok(services);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ServiceResponse>> GetByName([FromQuery] string name)
        {
            try
            {
                var services = await _query.GetByNameAsync(name);
                return Ok(services);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ServiceResponse>> CreateService([FromBody] CreateServiceRequest createRequestService)
        {
            try
            {
                var service = await _command.CreateService(createRequestService);
                return Ok(service);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<ServiceResponse>> UpdateService([FromQuery] int id, [FromBody] UpdateServiceRequest updateRequest)
        {
            try
            {
                var service = await _command.UpdateService(id, updateRequest);
                return Ok(service);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public override async Task<ActionResult<ServiceResponse>> DeleteService([FromQuery] int id)
        {
            try
            {
                var service = await _command.DeleteService(id);
                return Ok(service);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ServiceResponse>> AddOption([FromQuery] int id, [FromQuery] string name)
        {
            try
            {
                var service = await _command.AddOption(id,name);
                return Ok(service);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExistOption ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ServiceResponse>> DeleteOption([FromQuery] int id, [FromQuery] string name)
        {
            try
            {
                var service = await _command.DeleteOption(id,name);
                return Ok(service);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
