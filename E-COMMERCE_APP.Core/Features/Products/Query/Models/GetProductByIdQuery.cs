using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Products.Query.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Products.Query.Models
{
    public class GetProductByIdQuery : IRequest<Response<GetProductDTO>>
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public int Id { get; set; }
    }
}
