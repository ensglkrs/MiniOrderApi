using MiniOrderApi.Entities;

namespace MiniOrderApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void Add(Product product);
        List<Product> GetAll();
        Product? GetById(int id);
        void Update(Product product);
        void Delete(int id);
        (List<Product>, int) GetAll(int pageNumber, int pageSize, string? search, decimal? minPrice, decimal? maxPrice, string? sortBy, string? sortOrder);
    }
}
