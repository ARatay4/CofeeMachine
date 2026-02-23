using Coffee_Machine.Application.Exceptions;
using Coffee_Machine.Application.Features.Queries.Brew;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Coffee_Machine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeMachineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoffeeMachineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CoffeeMachineController>
        [HttpGet("/brew-coffee")]
        public async Task<IActionResult> GetBrewCoffee()
        {
            try
            {
                var result = await _mediator.Send(new GetBrewCoffeeQuery());

                if (result == null)
                    return StatusCode(StatusCodes.Status418ImATeapot);

                return Ok(new
                {
                    message = result.Message,
                    prepared = result.Prepared
                });
            }
            catch (MachineUnavailableException)
            {

                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        //// GET api/<CoffeeMachineController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CoffeeMachineController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CoffeeMachineController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CoffeeMachineController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
