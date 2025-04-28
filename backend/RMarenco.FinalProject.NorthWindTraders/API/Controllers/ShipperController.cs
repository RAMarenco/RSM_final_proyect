using Microsoft.AspNetCore.Mvc;
using NorthWindTraders.Application.Interfaces;

namespace NorthWindTraders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController(IShipperService shipperService) : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetShippers()
        {
            return Ok(await shipperService.GetAllShippers());
        }
    }
}
