using Microsoft.AspNetCore.Mvc;
using NorthWindTraders.Application.Interfaces;

namespace NorthWindTraders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await productService.GetAllProducts());
        }
    }
}
