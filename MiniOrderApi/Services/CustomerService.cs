using AutoMapper;
using MiniOrderApi.DTOs.Customer;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public void Add(CreateCustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);
            _customerRepository.Add(customer);
        }

        public List<CustomerResponse> GetAll()
        {
            var customers = _customerRepository.GetAllWithOrders();
            return _mapper.Map<List<CustomerResponse>>(customers);
        }

        public void Delete(int id)
        {
            var existingCustomer = _customerRepository.GetById(id);
            if (existingCustomer == null)
                throw new Exception("Customer not found.");

            _customerRepository.Delete(id);
        }

        public void Update(int id, CreateCustomerRequest request)
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
                throw new Exception("Customer not found.");

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;

            _customerRepository.Update(customer);
        }
    }
}