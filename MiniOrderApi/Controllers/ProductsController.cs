using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniOrderApi.DTOs.Product; // Yeni namespace
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CreateProductRequest request)
        {
            _productService.Add(request);
            return Ok(new { message = "Product added successfully!" });
        }



        [Authorize]
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] decimal? minPrice = null,
            [FromQuery] decimal? maxPrice = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = "asc"
        )
        {
            var result = _productService.GetAll(page, pageSize, search, minPrice, maxPrice, sortBy, sortOrder);
            return Ok(result);
        }
                

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, CreateProductRequest request)
        {
            _productService.Update(id, request);
            return Ok(new { message = "Product updated successfully!" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return NoContent();
        }
    }
}