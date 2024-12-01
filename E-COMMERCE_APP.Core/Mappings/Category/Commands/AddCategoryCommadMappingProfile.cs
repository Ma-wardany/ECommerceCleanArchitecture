using E_COMMERCE_APP.Core.Features.Categories.Commands.Models;
using E_COMMERCE_APP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Category
{
    public partial class CategoryMappingProfiles
    {
        public void AddCategoryCommadMappingProfile()
        {
            CreateMap<AddCtegoryCommand, Data.Entities.Category>().ReverseMap();
        }
    }
}
