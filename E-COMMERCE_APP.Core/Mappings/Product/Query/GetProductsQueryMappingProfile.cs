using E_COMMERCE_APP.Core.Features.Products.Query.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Product
{
    public partial class ProductMappingProfiles
    {
        public void GetProductsQueryMappingProfile()
        {
            CreateMap<Data.Entities.Product, GetProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
        }
    }
}
