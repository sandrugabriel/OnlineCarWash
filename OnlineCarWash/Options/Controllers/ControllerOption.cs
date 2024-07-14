using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Customers.Controllers.intefaces;
using OnlineCarWash.Customers.Dto;
using OnlineCarWash.Options.Controllers.interfaces;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.System.Exceptions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace OnlineCarWash.Options.Controllers
{
    public class ControllerOption : ControllerAPIOption
    {
        IOptionCommandService _command;
        IOptionQueryService _query;

        public ControllerOption(IOptionCommandService command, IOptionQueryService query)
        {
            _command = command;
            _query = query;
        }

        [Authorize]
        public override async Task<ActionResult<List<Option>>> GetAll()
        {
            try
            {
                var option = await _query.GetAllOption();
                return Ok(option);
            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }
        
        [Authorize]
        public override async Task<ActionResult<Option>> GetById([FromQuery] int id)
        {
            try
            {
                var option = await _query.GetByIdOption(id);
                return Ok(option);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<Option>> GetByName([FromQuery] string name)
        {
            try
            {
                var option = await _query.GetByNameOption(name);
                return Ok(option);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<Option>> Create([FromBody] CreateOptionRequest createOptionRequest)
        {
            try
            {
                var option = await _command.CreateOption(createOptionRequest);
                return Ok(option);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        public override async Task<ActionResult<Option>> Update([FromQuery] int id, [FromBody] UpdateOptionRequest updateOptionRequest)
        {
            try
            {
                var option = await _command.UpdateOption(id,updateOptionRequest);
                return Ok(option);
            }
            catch (InvalidPrice ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidName ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Authorize]
        public override async Task<ActionResult<Option>> Delete([FromQuery] int id)
        {
            try
            {
                var option = await _command.DeleteOption(id);
                return Ok(option);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
