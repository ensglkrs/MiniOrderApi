using Microsoft.EntityFrameworkCore;
using MiniOrderApi.Data;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;

namespace MiniOrderApi.Repositories.EntityFramework
{
    public class EfOrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public EfOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
    }
}