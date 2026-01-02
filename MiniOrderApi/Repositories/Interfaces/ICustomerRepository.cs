using MiniOrderApi.Entities;

namespace MiniOrderApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        List<Customer> GetAllWithOrders();
        Customer? GetById(int id);
        void Update(Customer customer);
        void Delete(int id);
        Customer? GetByUserId(int userId);
    }
}