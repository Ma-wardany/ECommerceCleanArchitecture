

using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Products.Command.Models;
using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;





namespace E_COMMERCE_APP.Core.Features.Products.Command.Handler
{
    public class ProductCommandsHandler : ResponseHandler,
                                          IRequestHandler<AddProductCommand, Response<string>>,
                                          IRequestHandler<DeleteProductCommand, Response<string>>,
                                          IRequestHandler<UpdateProductCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly IProductServices productServices;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductCommandsHandler(IMapper mapper, IProductServices productServices, IWebHostEnvironment webHostEnvironment)
        {
            this.mapper = mapper;
            this.productServices = productServices;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            string ImageUploadDirectory = Path.Combine(webHostEnvironment.WebRootPath ?? throw new InvalidOperationException("WebRootPath is null"), "Images");

            Directory.CreateDirectory(ImageUploadDirectory);

            List<ProductImages> ImagePaths = new();

            if (!(request.Images.IsNullOrEmpty()))
            {
                foreach (var img in request.Images)
                {
                    if (img != null && img.Length > 0)
                    {
                        string uniqueImgPath = Guid.NewGuid().ToString() + "_" + Path.GetFileName(img.FileName);
                        string imgPath = Path.Combine(ImageUploadDirectory, uniqueImgPath);

                        using var stream = new FileStream(imgPath, FileMode.Create);
                        await img.CopyToAsync(stream);

                        ImagePaths.Add(new ProductImages { ImgPath = imgPath });
                    }
                }
            }

            var product = mapper.Map<Product>(request) ?? throw new InvalidOperationException("Mapping failed.");
            product.Images = ImagePaths;

            string result = await productServices.AddProductAsync(product);

            return result switch
            {
                "NotFound" => NotFound<string>("There is no product to add!"),
                "Exist"    => BadRequest<string>("Product already exists!"),
                "Success"  => Success<string>("Product has been added successfully!"),
                _          => BadRequest<string>("Something went wrong!")
            };
        }


        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await productServices.DeleteProductAsync(request.Id);


            return result switch
            {
                "NotFound" => BadRequest<string>("Product not found!"),
                "Success" => Success<string>($"Product has id : {request.Id} has been deleted successfully!"),
                "Failed" or _ => BadRequest<string>("something went wrong!")
            };
        }

        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            var result = await productServices.UpdateProductAsync(product);

            return result switch
            {
                "NotFound" => NotFound<string>("there is no product!"),
                "NoUpdates" => BadRequest<string>("there ar no changes"),
                "Success" => Success<string>("Product has been updated successfully!"),
                _ => BadRequest<string>("something went wrong!")
            };
        }
    }
}
