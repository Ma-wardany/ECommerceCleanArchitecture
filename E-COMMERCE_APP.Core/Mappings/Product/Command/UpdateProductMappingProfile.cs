using E_COMMERCE_APP.Core.Features.Products.Command.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Product
{
    public partial class ProductMappingProfiles
    {
        public void UpdateProductMappingProfile()
        {
            CreateMap<UpdateProductCommand, Data.Entities.Product>().ReverseMap();
        }
    }
}
