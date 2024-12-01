using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Reviews
{
    public partial class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            AddReviewCommandMappingProfile();
            UpdateReviewCommandMappingProfile();
        }
    }
}
