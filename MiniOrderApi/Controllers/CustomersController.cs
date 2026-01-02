using Microsoft.AspNetCore.Mvc;
using MiniOrderApi.DTOs.Customer;
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public IActionResult Create(CreateCustomerRequest request)
        {
            _customerService.Add(request);
            return Ok(new { message = "Customer created successfully." });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _customerService.GetAll();
            return Ok(result);
        }
    }
}