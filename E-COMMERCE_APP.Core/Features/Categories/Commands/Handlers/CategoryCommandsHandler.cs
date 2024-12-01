using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Categories.Commands.Models;
using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Categories.Commands.Handlers
{
    internal class CategoryCommandsHandler : ResponseHandler,
                                             IRequestHandler<AddCtegoryCommand, Response<string>>,
                                             IRequestHandler<UpdateCategoryCommand, Response<string>>,
                                             IRequestHandler<DeleteCategoryCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly ICategoryServices categoryServices;

        public CategoryCommandsHandler(IMapper mapper, ICategoryServices categoryServices)
        {
            this.mapper = mapper;
            this.categoryServices = categoryServices;
        }

        public async Task<Response<string>> Handle(AddCtegoryCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(request);

            var result = await categoryServices.AddCategoryAsync(category);

            return result switch
            {
                "NotFound" => NotFound<string>("not category has been found!"),
                "Exist" => BadRequest<string>("this category is already exist!"),
                "Success" => Created<string>($"{request.Name} category has been added successfully!"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }

        public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(request);

            var result = await categoryServices.UpdateCategoryAsync(category);

            return result switch
            {
                "NotFound" => NotFound<string>("not category has been found!"),
                "NoUpdates" => BadRequest<string>("no changes occur!"),
                "Success" => Success<string>($"category with Id : {request.Id} has been updated successfully!"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await categoryServices.DeleteCategoryAsync(request.Id);

            return result switch
            {
                "NotFound" => NotFound<string>("not category has been found!"),
                "Success" => Success<string>($"category with Id : {request.Id} has been deleted successfully!"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }
    }
}
