using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Products.Query.Results;
using E_COMMERCE_APP.Core.Wrapper;
using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Products.Query.Models
{
    public class GetProductsByCategoryQuery : IRequest<Response<PaginatedResult<GetProductDTO>>>
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CategoryId must be greater than 0.")]
        public int CategoryId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 0.")]
        public int PageIndex { get; set; }

        [Required]
        public ProductFilterationEnum Filter { get; set; }
    }
}
