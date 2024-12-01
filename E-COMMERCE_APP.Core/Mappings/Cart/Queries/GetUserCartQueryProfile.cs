using AutoMapper;
using E_COMMERCE_APP.Core.Features.Cart.Queries.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Cart
{
    public partial class CartMappingProfiles
    {
        public void GetUserCartQueryProfile()
        {
            CreateMap<Data.Entities.Product, CartProduct>()
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.Name))
                .ReverseMap();
        }
    }
}
