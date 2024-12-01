using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Product
{
    public partial class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            AddProductCommandMappingProfile();
            UpdateProductMappingProfile();
            GetProductsQueryMappingProfile();
        }
    }
}
