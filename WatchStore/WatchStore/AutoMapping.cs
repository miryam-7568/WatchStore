using AutoMapper;
using DTOs;
using Entities;

namespace WatchStore
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RegisterUserDto, User>();
            CreateMap<LoginUserDto, User>();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ForCtorParam("CategoryName", opt => opt.MapFrom(src => src.Category.CategoryName));
            //CreateMap<OrderItem, OrderItemDto>(); //.ForCtorParam("ProductId", opt => opt.MapFrom(src => src.Product.Id));
            //CreateMap<Order, OrderDto>().ForCtorParam("Products", opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderDto, Order>().ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.Products));
            CreateMap<OrderItemDto, OrderItem>();
        }
    }
}


            
