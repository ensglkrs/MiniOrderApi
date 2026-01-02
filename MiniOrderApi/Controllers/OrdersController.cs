using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOrderApi.DTOs.Order;
using MiniOrderApi.Services.Interfaces;
using System.Security.Claims;

namespace MiniOrderApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Create(CreateOrderRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("Invalid token or missing User ID.");

            int userId = int.Parse(userIdClaim.Value);

            _orderService.Create(request, userId);

            return Ok(new { message = "Order created successfully." });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _orderService.GetAll();
            return Ok(result);
        }

        [HttpGet("customer/{customerId}")]
        public IActionResult GetByCustomerId(int customerId)
        {
            var orders = _orderService.GetByCustomerId(customerId);
            return Ok(orders);
        }
    }
}