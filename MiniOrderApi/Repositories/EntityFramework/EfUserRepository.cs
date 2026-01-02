using MiniOrderApi.Data;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;

namespace MiniOrderApi.Repositories.EntityFramework
{
    public class EfUserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public EfUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
    }
}