using AutoMapper;
using MiniOrderApi.DTOs;
using MiniOrderApi.DTOs.Product;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Add(CreateProductRequest request)
        {
            var productEntity = _mapper.Map<Product>(request);
            _repository.Add(productEntity);
        }

        public ServiceResponse<List<ProductResponse>> GetAll(int pageNumber, int pageSize, string? search, decimal? minPrice, decimal? maxPrice, string? sortBy, string? sortOrder)
        {
            var result = _repository.GetAll(pageNumber, pageSize, search, minPrice, maxPrice, sortBy, sortOrder);

            var productDtos = _mapper.Map<List<ProductResponse>>(result.Item1); 

            var totalRecords = result.Item2; 

            var response = new PagedResponse<List<ProductResponse>>(productDtos, pageNumber, pageSize, totalRecords);

            return response;
        }

        public void Update(int id, CreateProductRequest request)
        {
            var product = _repository.GetById(id);
            if (product == null)
                throw new Exception("Product not found");

            product.Name = request.Name;
            product.Price = request.Price;

            _repository.Update(product);
        }

        public void Delete(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
                throw new Exception("Product not found");

            _repository.Delete(id);
        }
    }
}