using AutoMapper;
using MiniOrderApi.DTOs.Auth;
using MiniOrderApi.DTOs.Customer;
using MiniOrderApi.DTOs.Order;
using MiniOrderApi.DTOs.Product;
using MiniOrderApi.Entities;

namespace MiniOrderApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.Role) ? "User" : src.Role));

            CreateMap<RegisterRequest, Customer>();

            CreateMap<CreateCustomerRequest, Customer>();

            CreateMap<Customer, CustomerResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, ProductResponse>();

            CreateMap<Order, OrderResponse>();
        }
    }
}