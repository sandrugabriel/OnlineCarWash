using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Services.Dto;

namespace OnlineCarWash.Services.Controller.interfaces
{

    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPIService : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<ServiceResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<ServiceResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<ServiceResponse>> GetById([FromQuery] int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<ServiceResponse>> GetByName([FromQuery] string name);

        [HttpPost("CreateService")]
        [ProducesResponseType(statusCode: 201, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<ServiceResponse>> CreateService([FromBody] CreateServiceRequest createRequestService);

        [HttpPut("UpdateService")]
        [ProducesResponseType(statusCode: 200, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<ServiceResponse>> UpdateService([FromQuery] int id, [FromBody] UpdateServiceRequest updateRequest);

        [HttpDelete("DeleteService")]
        [ProducesResponseType(statusCode: 200, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<ServiceResponse>> DeleteService([FromQuery] int id);

        [HttpPut("AddOption")]
        [ProducesResponseType(statusCode: 201, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<ServiceResponse>> AddOption([FromQuery]int id, [FromBody]string name);

        [HttpPut("DeleteOption")]
        [ProducesResponseType(statusCode: 201, type: typeof(ServiceResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<ServiceResponse>> DeleteOption([FromQuery] int id, [FromQuery] string name);



    }
}
