using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Cart
{
    public partial class CartMappingProfiles : Profile
    {
        public CartMappingProfiles()
        {
            AddCartCommandMappingProfile();
            UpdateCartItemMappingProfile();
            DeleteCartItemMappingProfile();
            GetUserCartQueryProfile();
        }
    }
}
