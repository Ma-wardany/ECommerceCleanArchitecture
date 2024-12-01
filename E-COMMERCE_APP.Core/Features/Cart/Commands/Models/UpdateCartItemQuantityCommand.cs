using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Commands.Models
{
    public class UpdateCartItemQuantityCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
