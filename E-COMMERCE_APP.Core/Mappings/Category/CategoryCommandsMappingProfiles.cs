﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Category
{
    public partial class CategoryMappingProfiles : Profile
    {
        public CategoryMappingProfiles()
        {
            AddCategoryCommadMappingProfile();
            UpdateCategoryCommandMappingProfile();
        }
    }
}