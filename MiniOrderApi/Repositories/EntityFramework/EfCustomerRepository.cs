using Microsoft.EntityFrameworkCore;
using MiniOrderApi.Data;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;

namespace MiniOrderApi.Repositories.EntityFramework
{
    public class EfCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public EfCustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public List<Customer> GetAllWithOrders()
        {
            return _context.Customers.Include(c => c.Orders).ToList();
        }

        public Customer? GetById(int id)
        {
            return _context.Customers.Include(c => c.Orders).FirstOrDefault(c => c.Id == id);
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public Customer? GetByUserId(int userId)
        {
            return _context.Customers.FirstOrDefault(c => c.UserId == userId);
        }
    }
}