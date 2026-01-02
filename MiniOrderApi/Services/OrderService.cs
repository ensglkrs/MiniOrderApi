using AutoMapper;
using MiniOrderApi.DTOs.Order;
using MiniOrderApi.Entities;
using MiniOrderApi.Repositories.Interfaces;
using MiniOrderApi.Services.Interfaces;

namespace MiniOrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public void Create(CreateOrderRequest request, int userId)
        {
            var customer = _customerRepository.GetByUserId(userId);

            if (customer == null)
                throw new Exception("Customer profile not found for creating an order.");

            decimal calculatedTotal = 0;
            var orderItems = new List<OrderItem>();

            foreach (var itemRequest in request.Items)
            {
                var product = _productRepository.GetById(itemRequest.ProductId);

                if (product == null)
                    throw new Exception($"Product with ID {itemRequest.ProductId} not found.");

                if (product.Stock < itemRequest.Quantity)
                    throw new Exception($"Not enough stock for '{product.Name}'. Available: {product.Stock}");

                product.Stock -= itemRequest.Quantity;
                _productRepository.Update(product);

                var currentPrice = product.Price * itemRequest.Quantity;
                calculatedTotal += currentPrice;

                orderItems.Add(new OrderItem
                {
                    ProductId = itemRequest.ProductId,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = product.Price
                });
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.UtcNow,
                TotalPrice = calculatedTotal,
                OrderItems = orderItems
            };

            _orderRepository.Add(order);
        }

        public List<OrderResponse> GetAll()
        {
            var orders = _orderRepository.GetAll();
            return _mapper.Map<List<OrderResponse>>(orders);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderResponse> GetByCustomerId(int customerId)
        {
            var allOrders = _orderRepository.GetAll();
            var customerOrders = allOrders.Where(o => o.CustomerId == customerId).ToList();
            return _mapper.Map<List<OrderResponse>>(customerOrders);
        }
    }
}