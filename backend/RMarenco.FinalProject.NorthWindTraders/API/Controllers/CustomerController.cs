using Microsoft.AspNetCore.Mvc;
using NorthWindTraders.Application.Interfaces;

namespace NorthWindTraders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await customerService.GetAllCustomers());
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCustomerById([FromRoute] string id)
        {
            return Ok(await customerService.GetCustomerWithOrders(id));
        }
    }
}
