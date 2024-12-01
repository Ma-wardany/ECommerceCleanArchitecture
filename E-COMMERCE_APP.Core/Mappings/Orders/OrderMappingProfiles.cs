using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Mappings.Orders
{
    public partial class OrderMappingProfiles : Profile
    {
        public OrderMappingProfiles()
        {
            PlaceOrderCommandMappingProfile();
        }
    }
}
