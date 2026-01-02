using MiniOrderApi.DTOs;
using MiniOrderApi.DTOs.Product;

namespace MiniOrderApi.Services.Interfaces
{
    public interface IProductService
    {
        void Add(CreateProductRequest request);
        void Update(int id, CreateProductRequest request);
        void Delete(int id);
        ServiceResponse<List<ProductResponse>> GetAll(int pageNumber, int pageSize, string? search, decimal? minPrice, decimal? maxPrice, string? sortBy, string? sortOrder);
    }
}
