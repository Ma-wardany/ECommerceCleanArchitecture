using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IOrderServices
    {
        public Task<(Order?, string?)> PlaceOrder(string UserId);
    }
}
