using AutoMapper;
using E_COMMERCE_APP.Core.Bases;
using E_COMMERCE_APP.Core.Features.Reviews.Queries.Models;
using E_COMMERCE_APP.Core.Features.Reviews.Results;
using E_COMMERCE_APP.Services.Abstracts;
using MediatR;

namespace E_COMMERCE_APP.Core.Features.Reviews.Queries.Handler
{
    public class ReviewQueriesHandler : ResponseHandler,
                                        IRequestHandler<GetUserReviewsQuery, Response<List<ReviewResult>>>,
                                        IRequestHandler<GetProductReviewsQuery, Response<List<ReviewResult>>>
    {
        private readonly IMapper mapper;
        private readonly IReviewServices reviewServices;

        public ReviewQueriesHandler(IMapper mapper, IReviewServices reviewServices)
        {
            this.mapper         = mapper;
            this.reviewServices = reviewServices;
        }

        public async Task<Response<List<ReviewResult>>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            var result  = await reviewServices.GetProductReviewsAsync(request.ProductId);
            var reviews = result.Item1;
            var message = result.Item2;

            if(message != null)
            {
                return message switch
                {
                    "NotFound" => BadRequest<List<ReviewResult>>("Product is not exist!"),
                    "Empty"    => Success<List<ReviewResult>>(null, message : "No any review for this product!"),
                    _          => BadRequest<List<ReviewResult>>(message)
                };
            }

            var reviewsResult = mapper.Map<List<ReviewResult>>(reviews);

            return Success<List<ReviewResult>>(reviewsResult);
        }

        public async Task<Response<List<ReviewResult>>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
        {
            var result = await reviewServices.GetUserReviewsAsync(request.UserId);
            var reviews = result.Item1;
            var message = result.Item2;

            if (message != null)
            {
                return message switch
                {
                    "UnAuthorized" => BadRequest<List<ReviewResult>>("Unauthorized user!"),
                    "Empty" => Success<List<ReviewResult>>(null, message: "No any review made by this user!"),
                    _ => BadRequest<List<ReviewResult>>(message)
                };
            }

            var reviewsResult = mapper.Map<List<ReviewResult>>(reviews);

            return Success<List<ReviewResult>>(reviewsResult);
        }
    }
}
