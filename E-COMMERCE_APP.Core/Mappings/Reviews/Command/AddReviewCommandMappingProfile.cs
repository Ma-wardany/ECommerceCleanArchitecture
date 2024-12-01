using AutoMapper;
using E_COMMERCE_APP.Core.Features.Reviews.Commands.Models;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Reviews
{
    public partial class ReviewMappingProfile
    {
        public void AddReviewCommandMappingProfile()
        {
            CreateMap<AddReviewCommand, Review>().ReverseMap();

            CreateMap<Review, ReviewResult>()
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.applicationUser.UserName))
                .ForMember(dest => dest.ProductName, options => options.MapFrom(src => src.Product.Name))
                .ReverseMap();
        }
    }
}
