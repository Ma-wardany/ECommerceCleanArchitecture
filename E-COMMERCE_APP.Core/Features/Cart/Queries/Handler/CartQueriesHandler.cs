using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Cart.Queries.Models;
using E_COMMERCE_APP.Core.Features.Cart.Queries.Results;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Cart.Queries.Handler
{
    public class CartQueriesHandler : ResponseHandler,
                                      IRequestHandler<GetUserCartQuery, Response<UserCartResult>>
    {
        private readonly IMapper mapper;
        private readonly ICartServices cartServices;

        public CartQueriesHandler(IMapper mapper, ICartServices cartServices)
        {
            this.mapper = mapper;
            this.cartServices = cartServices;
        }

        public async Task<Response<UserCartResult>> Handle(GetUserCartQuery request, CancellationToken cancellationToken)
        {
            var result = await cartServices.GetUserCarts(request.UserId);

            string? message = result.Item2;
            var Carts = result.Item1;

            if (message != null) // If there's an error message in Item2
            {
                return result.Item2 switch
                {
                    "NotFoundUser" => BadRequest<UserCartResult>("user not found"),
                    "EmptyCart" => BadRequest<UserCartResult>("empty cart!"),
                    _ => BadRequest<UserCartResult>("something went wrong!")
                };
            }

            List<CartProduct> cartProducts = Carts.Select(cart =>
            {
                var cartProduct = mapper.Map<CartProduct>(cart.Product);
                cartProduct.Quantity = cart.Quantity;
                cartProduct.CategoryName = cart.Product.Category.Name;
                
                return cartProduct;

            }).ToList();

            var userCart = new UserCartResult
            {
                UserId = request.UserId,
                Products = cartProducts,
                TotalPayment = cartProducts.Sum(c => (c.Price * c.Quantity))
            };

            return Success(userCart);
        }
    }
}
