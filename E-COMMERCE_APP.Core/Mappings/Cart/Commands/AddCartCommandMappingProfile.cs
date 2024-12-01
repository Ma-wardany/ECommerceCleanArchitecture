using AutoMapper;
using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Cart
{
    public partial class CartMappingProfiles
    {
        public void AddCartCommandMappingProfile()
        {
            CreateMap<AddCartCommand, Data.Entities.Cart>()
                .ForMember(dest => dest.Date, options => options.MapFrom(src => DateTime.UtcNow))
                .ReverseMap();
        }
    }
}
