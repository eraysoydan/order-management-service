using AutoMapper;
using OrderManagement.API.Models;
using OrderManagement.API.Models.Entity;

namespace OrderManagement.API.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, CreateOrderRequestItem>()
                .ForPath(dest => dest.OrderItem.Code, opt => opt.MapFrom(src => src.ItemCode));

            CreateMap<CreateOrderRequestItem, Order>()
                .ForPath(dest => dest.ItemCode, opt => opt.MapFrom(src => src.OrderItem.Code));
        }
    }
}
