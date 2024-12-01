using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Commands.Models;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Core.Features.Reviews.Commands.Handler
{
    internal class ReviewCommandHandler : ResponseHandler,
                                          IRequestHandler<AddReviewCommand, Response<ReviewResult>>,
                                          IRequestHandler<DeleteReviewCommand, Response<string>>,
                                          IRequestHandler<UpdateReviewCommad, Response<ReviewResult>>
    {

        private readonly IMapper mapper;
        private readonly IReviewServices reviewServices;

        public ReviewCommandHandler(IMapper mapper, IReviewServices reviewServices)
        {
            this.mapper         = mapper;
            this.reviewServices = reviewServices;
        }

        /*------------------------------------------------------------------------------------------------------------*/

        public async Task<Response<ReviewResult>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var MappedReview = mapper.Map<Review>(request);

            var result  = await reviewServices.AddReviewAsync(MappedReview);
            var review  = result.Item1;
            var message = result.Item2;

            if(message != null)
            {
                return message switch
                {
                    "UnAuthorized" => UnAuthorized<ReviewResult>("un authorized user"),
                    "NotFound"     => BadRequest<ReviewResult>("review not found!"),
                    "Reviewed"     => BadRequest<ReviewResult>("you are already reviewed this product!"),
                    _              => BadRequest<ReviewResult>(message)
                };
            }

            var ReviewResult = mapper.Map<ReviewResult>(review);

            return Created<ReviewResult>(ReviewResult);
        }

        /*------------------------------------------------------------------------------------------------------------*/

        public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var result = await reviewServices.DeleteReviewAsync(request.UserId, request.ReviewId);

            return result switch
            {
                "UnAuthorized" => UnAuthorized<string>("unauthorized user"),
                "NotFound"     => BadRequest<string>("review not found!"),
                "Deleted"      => Success<string>("review has been deleted successfully!"),
                _              => BadRequest<string>(result)
            };
        }

        /*------------------------------------------------------------------------------------------------------------*/

        public async Task<Response<ReviewResult>> Handle(UpdateReviewCommad request, CancellationToken cancellationToken)
        {
            var MappedReview = mapper.Map<Review>(request);

            var result  = await reviewServices.UpdateReviewAsync(MappedReview);
            var review  = result.Item1;
            var message = result.Item2;

            if(message != null)
            {
                return message switch
                {
                    "UnAuthorized" => UnAuthorized<ReviewResult>("unauthorized user"),
                    "NotFound"     => BadRequest<ReviewResult>("review not found!"),
                    _              => BadRequest<ReviewResult>(message)
                };
            }

            var ReviewResult = mapper.Map<ReviewResult>(review);
            return Success<ReviewResult>(ReviewResult);
        }
    }
}
