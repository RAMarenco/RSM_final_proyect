using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthWindTraders.Application.DTOs;
using NorthWindTraders.Application.DTOs.Order;
using NorthWindTraders.Application.Interfaces;

namespace NorthWindTraders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService, IOrderDetailService orderDetailService) : ControllerBase
    {
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
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
