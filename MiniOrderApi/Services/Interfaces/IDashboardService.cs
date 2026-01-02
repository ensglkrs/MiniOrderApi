using MiniOrderApi.DTOs.Dashboard;

namespace MiniOrderApi.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardStatsResponse GetStats();
    }
}