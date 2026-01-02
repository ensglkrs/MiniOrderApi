using MiniOrderApi.Entities;

namespace MiniOrderApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        List<Order> GetAll();
    }
}