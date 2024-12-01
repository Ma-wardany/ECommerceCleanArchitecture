using E_COMMERCE_APP.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Categories.Commands.Models
{
    public class AddCtegoryCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
