using MiniOrderApi.Data;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;

namespace MiniOrderApi.Repositories.EntityFramework
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public EfProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product? GetById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        public (List<Product>, int) GetAll(int pageNumber, int pageSize, string? search, decimal? minPrice, decimal? maxPrice, string? sortBy, string? sortOrder)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            int totalRecords = query.Count();

            switch (sortBy?.ToLower())
            {
                case "price":
                    query = sortOrder == "desc" ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);
                    break;
                case "name":
                    query = sortOrder == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name);
                    break;
                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }

            var list = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (list, totalRecords);
        }
    }
}