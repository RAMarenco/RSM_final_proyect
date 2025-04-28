using Microsoft.AspNetCore.Mvc;
using NorthWindTraders.Application.Interfaces;

namespace NorthWindTraders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1)
        {
            return Ok(await orderService.GetAllOrders(pageNumber));
        }
    }
}
