using E_COMMERCE_APP.Core.Features.Categories.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Category
{
    public partial class CategoryMappingProfiles
    {
        public void UpdateCategoryCommandMappingProfile()
        {
            CreateMap<UpdateCategoryCommand, Data.Entities.Category>().ReverseMap();
        }
    }
}
