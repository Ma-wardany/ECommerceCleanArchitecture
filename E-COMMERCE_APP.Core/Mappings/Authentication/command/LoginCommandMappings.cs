using E_COMMERCE_APP.Core.Features.Authentication.Commands.Models;
using E_COMMERCE_APP.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Authentication
{
    public partial class AuthenticationProfile
    {
        public void AddLoginCommandMappings()
        {
            CreateMap<LoginCommand, ApplicationUser>().ReverseMap();
        }
    }
}
