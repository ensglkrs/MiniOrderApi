using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Controllers
{
    [Authorize(Roles = "Admin")] // Critical Security Check!
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _dashboardService.GetStats();
            return Ok(stats);
        }
    }
}