using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Products.Query.Models;
using E_COMMERCE_APP.Core.Features.Products.Query.Results;
using E_COMMERCE_APP.Core.Wrapper;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;


namespace E_COMMERCE_APP.Core.Features.Products.Query.Handler
{
    public class ProductQueriesHandler : ResponseHandler,
                 IRequestHandler<GetProductsByCategoryQuery, Response<PaginatedResult<GetProductDTO>>>,
                 IRequestHandler<GetProductByIdQuery, Response<GetProductDTO>>,
                 IRequestHandler<GetProductByNameQuery, Response<GetProductDTO>>
    {
        private readonly IMapper mapper;
        private readonly IProductServices productServices;

        public ProductQueriesHandler(IMapper mapper, IProductServices productServices)
        {
            this.mapper = mapper;
            this.productServices = productServices;
        }

        public async Task<Response<PaginatedResult<GetProductDTO>>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            // Await the asynchronous method to get the products
            var products = productServices.GetProductsByCategoryAsync(request.CategoryId, request.Filter);
            
            if (!products.Any())
                return BadRequest<PaginatedResult<GetProductDTO>>("No products found for the given category.");

            int totalPages = (int)Math.Ceiling((double)products.Count() / 10); // 10 is the page size
            if (request.PageIndex > totalPages)
            {
                return BadRequest<PaginatedResult<GetProductDTO>>("This page index contains nothing.");
            }

            var productsDto = mapper.ProjectTo<GetProductDTO>(products.AsQueryable());

            var paginatedProductDto = await PaginatedResult<GetProductDTO>.CreateAsync(productsDto, request.PageIndex);

            return Success(paginatedProductDto);
           
        }

        public async Task<Response<GetProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productServices.GetProductByIdAsync(request.Id);
            if (product == null) return BadRequest<GetProductDTO>("No product with this Id");

            var productDTO = mapper.Map<GetProductDTO>(product);
            return Success<GetProductDTO>(productDTO);
        }

        public async Task<Response<GetProductDTO>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await productServices.GetProductByNameAsync(request.Name);
            if (product == null) return BadRequest<GetProductDTO>("No product with this Name");

            var productDTO = mapper.Map<GetProductDTO>(product);
            return Success<GetProductDTO>(productDTO);
        }
    }
}
