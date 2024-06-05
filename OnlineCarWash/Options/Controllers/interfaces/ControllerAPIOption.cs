using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Options.Dto;
using OnlineCarWash.Options.Models;

namespace OnlineCarWash.Options.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIOption : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode:200,type:typeof(List<Option>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Option>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Option>> GetById([FromQuery]int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Option>> GetByName([FromQuery] string name);

        [HttpPost("CreateOption")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Option>> Create([FromBody]CreateOptionRequest createOptionRequest);

        [HttpPut("UpdateOption")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Option>> Update([FromQuery]int id,[FromBody] UpdateOptionRequest updateOptionRequest);

        [HttpDelete("DeleteOption")]
        [ProducesResponseType(statusCode: 200, type: typeof(Option))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Option>> Delete([FromQuery] int id);
    }
}
