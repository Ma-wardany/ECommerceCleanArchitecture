using E_COMMERCE_APP.Core.Features.Products.Command.Models;
using E_COMMERCE_APP.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Product
{
    public partial class ProductMappingProfiles
    {
        public void AddProductCommandMappingProfile()
        {
            CreateMap<AddProductCommand, Data.Entities.Product>()
                       .ForMember(dest => dest.Images, opt => opt.Ignore())  // Ignore Images during mapping
                       .ReverseMap();
        }
    }
}