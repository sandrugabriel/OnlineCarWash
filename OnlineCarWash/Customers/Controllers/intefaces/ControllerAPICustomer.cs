﻿using Microsoft.AspNetCore.Mvc;
using OnlineCarWash.Customers.Dto;

namespace OnlineCarWash.Customers.Controllers.intefaces
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public abstract class ControllerAPICustomer : ControllerBase
    {

        [HttpGet("All")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<CustomerResponse>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<CustomerResponse>>> GetAll();

        [HttpGet("FindById")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CustomerResponse>> GetById([FromQuery]int id);

        [HttpGet("FindByName")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<CustomerResponse>> GetByName([FromQuery] string name);

        [HttpPost("CreateCustomer")]
        [ProducesResponseType(statusCode: 201, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> RegisterCustomer([FromBody] CreateCustomerRequest createRequestCustomer);

        [HttpPost("LoginCustomer")]
        [ProducesResponseType(statusCode: 201, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> LoginCustomer([FromBody] LoginRequest request);


        [HttpPut("UpdateCustomer")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> UpdateCustomer([FromQuery] int id, [FromBody] UpdateCustomerRequest updateRequest);

        [HttpDelete("DeleteCustomer")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> DeleteCustomer([FromQuery] int id);

        [HttpPut("AddAppointment")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> AddAppointment([FromQuery] int id, AddAppointmentRequest request);
       
        [HttpPut("DeleteAppointment")]
        [ProducesResponseType(statusCode: 200, type: typeof(CustomerResponse))]
        [ProducesResponseType(statusCode: 400, type: typeof(string))]
        [ProducesResponseType(statusCode: 404, type: typeof(string))]
        public abstract Task<ActionResult<CustomerResponse>> DeleteAppointment([FromQuery] int id, [FromQuery] string nameService, [FromQuery] string nameOption);

    }
}
