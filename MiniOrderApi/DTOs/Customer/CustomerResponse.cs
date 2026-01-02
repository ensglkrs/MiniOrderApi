using MiniOrderApi.DTOs.Order; 

namespace MiniOrderApi.DTOs.Customer
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<OrderResponse> Orders { get; set; } = new();
    }
}