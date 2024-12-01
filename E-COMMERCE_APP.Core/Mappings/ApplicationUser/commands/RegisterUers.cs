using E_COMMERCE_APP.Core.Features.Accounts.Commands.CommandModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Core.Features.Accounts.Commands.Results;

namespace E_COMMERCE_APP.Core.Mappings.ApplicationUserProfile
{
    public partial class ApplicationUserProfile
    {
        public void RegisterUserMappings()
        {
            
            CreateMap<RegisterCommand, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, RegisterationResultModel>()
                .ForMember(dest => dest.Name, options => options.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.UserName, options => options.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsConfirmed, options => options.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
