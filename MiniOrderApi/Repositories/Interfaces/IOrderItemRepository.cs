using MiniOrderApi.Entities;

namespace MiniOrderApi.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        void AddRange(List<OrderItem> items);
        List<OrderItem> GetByOrderId(int orderId);
        void DeleteByOrderId(int orderId);
    }
}
