using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Cart.Commands.Models;
using E_COMMERCE_APP.Services.Abstracts;
      
using E_COMMERCE_APP.Data.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace E_COMMERCE_APP.Core.Features.Cart.Commands.Handler
{
    public class CartCommandsHandler : ResponseHandler,
                                       IRequestHandler<AddCartCommand, Response<string>>,
                                       IRequestHandler<DeleteCartItemCommand, Response<string>>,
                                       IRequestHandler<UpdateCartItemQuantityCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly ICartServices cartServices;

        public CartCommandsHandler(IMapper mapper, ICartServices cartServices)
        {
            this.mapper = mapper;
            this.cartServices = cartServices;
        }

        public async Task<Response<string>> Handle(AddCartCommand request, CancellationToken cancellationToken)
        {
            var cartItem = mapper.Map<Data.Entities.Cart>(request);

            var result = await cartServices.AddCartItem(cartItem);

            return result switch
            {
                "NotFound" => NotFound<string>("no cart found"),
                "NotFoundProduct" => BadRequest<string>("this product not found"),
                "NoQuatity" => BadRequest<string>("Insufficient product quantity!"),
                "Updated" => Success<string>("quantity has been updated successfully"),
                "Added" => Created<string>("product has been added to cart successfully!"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }

        public async Task<Response<string>> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cartItem = mapper.Map<Data.Entities.Cart>(request);

            var result = await cartServices.UpdateCartItemQuantity(cartItem);

            return result switch
            {
                "NotFound" => NotFound<string>("no cart found"),
                "NoUpdates" => BadRequest<string>($"quantity is still = {request.Quantity} (not updated)"),
                "NoQuatity" => BadRequest<string>($"Insufficient product quantity (available quantity less than {request.Quantity})!"),
                "Success" => Success<string>("quantity has been updated successfully"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }

        public async Task<Response<string>> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var cartItem = mapper.Map<Data.Entities.Cart>(request);

            var result = await cartServices.DeleteCartItem(request.ProductId, request.UserId);

            return result switch
            {
                "NotFound" => NotFound<string>("no cart found"),
                "Success" => Success<string>("quantity has been deleted successfully"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }
    }
}
