﻿using E_COMMERCE_APP.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Products.Command.Models
{
    public class AddProductCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
