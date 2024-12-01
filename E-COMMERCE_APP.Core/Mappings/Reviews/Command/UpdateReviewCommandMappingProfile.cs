using AutoMapper;
using E_COMMERCE_APP.Core.Features.Reviews.Commands.Models;
using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Reviews
{
    public partial class ReviewMappingProfile : Profile
    {
        public void UpdateReviewCommandMappingProfile()
        {
            CreateMap<UpdateReviewCommad, Review>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.ReviewId));
        }
    }
}
