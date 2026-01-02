using Microsoft.EntityFrameworkCore;
using MiniOrderApi.Data;
using MiniOrderApi.DTOs.Dashboard;
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public DashboardStatsResponse GetStats()
        {
            // 1. Calculate Total Revenue (Handle empty table case)
            var totalRevenue = _context.Orders.Any() ? _context.Orders.Sum(o => o.TotalPrice) : 0;

            // 2. Count Total Orders
            var totalOrders = _context.Orders.Count();

            // 3. Count Total Products
            var totalProducts = _context.Products.Count();

            // 4. Find Best Selling Product (Grouping by ProductId)
            var bestSellerName = "N/A";

            var bestSellerGroup = _context.OrderItems
                .GroupBy(x => x.ProductId)
                .Select(g => new { ProductId = g.Key, TotalSold = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.TotalSold)
                .FirstOrDefault();

            if (bestSellerGroup != null)
            {
                var product = _context.Products.Find(bestSellerGroup.ProductId);
                if (product != null)
                {
                    bestSellerName = product.Name;
                }
            }

            return new DashboardStatsResponse
            {
                TotalRevenue = totalRevenue,
                TotalOrders = totalOrders,
                TotalProducts = totalProducts,
                BestSellingProduct = bestSellerName
            };
        }
    }
}