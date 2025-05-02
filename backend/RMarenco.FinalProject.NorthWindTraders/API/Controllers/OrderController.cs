using Microsoft.AspNetCore.Mvc;
using NorthWindTraders.Application.CustomExceptions;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;

namespace NorthWindTraders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                   .SelectMany(v => v.Errors)
                   .Select(e => e.ErrorMessage)
                   .ToList();
                throw new BadRequestException("Validation errors occurred.", errorMessages);
            }
            return Ok($"Order: {await orderService.AddOrder(createOrderDto)} added successfully");
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetOrders([FromQuery] int pageNumber = 1)
        {
            return Ok(await orderService.GetAllOrders(pageNumber));
        }

        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            return Ok(await orderService.GetOrderWithDetails(id));
        }

        [HttpGet]
        [Route("report")]
        public async Task<IActionResult> GetOrderReport()
        {
            byte[] pdfBytes = await orderService.GenerateAllOrderReport();

            if (pdfBytes == null || pdfBytes.Length == 0)
                return NotFound("Report generation failed or no data found.");

            return File(pdfBytes, "application/pdf", "OrderReport_.pdf");
        }

        [HttpGet]
        [Route("report/{id:int}")]
        public async Task<IActionResult> GetOrderByIDReport(int id)
        {
            byte[] pdfBytes = await orderService.GenerateOrderReport(id);

            if (pdfBytes == null || pdfBytes.Length == 0)
                return NotFound("Report generation failed or no data found.");

            return File(pdfBytes, "application/pdf", $"OrderReport_{id}.pdf");
        }

        [HttpPut]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderDto updateOrderDto)
        {
            return Ok($"Order: {await orderService.UpdateOrder(id, updateOrderDto)} updated successfully");
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await orderService.DeleteOrder(id);
            return Ok($"Order: {id} deleted successfully");
        }
    }
}
