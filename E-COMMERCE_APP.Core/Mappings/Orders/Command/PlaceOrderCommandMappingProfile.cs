using AutoMapper;
using E_COMMERCE_APP.Core.Features.Order.Results;
using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Orders
{
    public partial class OrderMappingProfiles
    {
        public void PlaceOrderCommandMappingProfile()
        {
            CreateMap<Order, PlaceOrderResult>()
                .ForMember(dest => dest.Products, options => options.MapFrom(src => src.OrderProducts));

            CreateMap<OrderProduct, ProductOrderDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.product.Name))
                .ForMember(dest => dest.Price, options => options.MapFrom(src => src.product.Price))
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.product.Category.Name))
                .ForMember(dest => dest.Qunatity, options => options.MapFrom(src => src.Quantity))
                .ReverseMap();
        }
    }
}
