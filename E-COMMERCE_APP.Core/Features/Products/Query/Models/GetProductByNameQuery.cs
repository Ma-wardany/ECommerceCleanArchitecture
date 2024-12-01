using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Products.Query.Results;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace E_COMMERCE_APP.Core.Features.Products.Query.Models
{
    public class GetProductByNameQuery : IRequest<Response<GetProductDTO>>
    {
        [Required]
        public string Name { get; set; }
    }
}
