using MiniOrderApi.DTOs.Order;

namespace MiniOrderApi.Services.Interfaces
{
    public interface IOrderService
    {
        void Create(CreateOrderRequest request, int userId);
        List<OrderResponse> GetAll();
        void Delete(int id);
        List<OrderResponse> GetByCustomerId(int customerId);
    }
}
