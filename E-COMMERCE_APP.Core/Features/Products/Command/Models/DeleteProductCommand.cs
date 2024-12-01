using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Products.Command.Models
{
    public class DeleteProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
