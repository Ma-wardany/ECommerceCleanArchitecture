using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Cart.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Queries.Models
{
    public class GetUserCartQuery : IRequest<Response<UserCartResult>>
    {
        public string UserId { get; set; }
    }
}
