using MiniOrderApi.Entities;
using MiniOrderApi.DTOs.Customer;

namespace MiniOrderApi.Services.Interfaces
{
    public interface ICustomerService
    {
        void Add(CreateCustomerRequest request);
        List<CustomerResponse> GetAll();
        void Update(int id, CreateCustomerRequest request);
        void Delete(int id);
    }
}
