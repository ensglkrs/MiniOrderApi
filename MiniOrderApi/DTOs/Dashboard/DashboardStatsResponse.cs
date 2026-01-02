namespace MiniOrderApi.DTOs.Dashboard
{
    public class DashboardStatsResponse
    {
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public string BestSellingProduct { get; set; } = string.Empty;
    }
}