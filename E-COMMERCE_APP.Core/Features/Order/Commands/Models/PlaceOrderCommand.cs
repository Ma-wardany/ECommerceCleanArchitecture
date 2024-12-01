using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Order.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Order.Commands.Models
{
    public class PlaceOrderCommand : IRequest<Response<PlaceOrderResult>>
    {
        public string UserId { get; set; }
    }
}
