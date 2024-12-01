using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IProductRepository productRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewServices(
            IReviewRepository reviewRepository,
            IProductRepository productRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewRepository = reviewRepository;
            this.productRepository = productRepository;
            this.userManager = userManager;
        }

        public async Task<(Review?, string?)> AddReviewAsync(Review review)
        {
            var user = await userManager.FindByIdAsync(review.UserId);

            if (user == null) 
                return (null, "UnAuthorized");

            var ExistReview = reviewRepository
                .GetTableNoTracking()
                .FirstOrDefault(r => r.UserId == review.UserId && r.ProductId == review.ProductId);

            if (ExistReview != null) 
                return (null, "Reviewed");

            using var Trans = await reviewRepository.BeginTransactionAsync();
            try
            {
                var NewReview = await reviewRepository.AddAsync(review);

                // Explicitly load the navigation properties
                await reviewRepository.GetTableAsTracking().Include(r => r.Product)
                                                           .Include(r => r.applicationUser)
                                                           .Where(r => r.Id == NewReview.Id)
                                                           .FirstOrDefaultAsync();
                await Trans.CommitAsync();

                return (NewReview, null);
            }
            catch (Exception ex)
            {
                await Trans.RollbackAsync();
                return(null, ex.Message);
            }            
        }

        public async Task<string> DeleteReviewAsync(string UserId, int reviewId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null) 
                return "UnAuthorized";

            var review = await reviewRepository.GetByIdAsync(reviewId);

            if (review == null) 
                return "NotFound";

            using var Trans = await reviewRepository.BeginTransactionAsync();

            try
            {
                await reviewRepository.DeleteAsync(review);
                await Trans.CommitAsync();
                return "Deleted";
            }
            catch (Exception ex)
            {
                await Trans.RollbackAsync();
                return ex.Message;
            }
        }

        public async Task<(Review?, string?)> UpdateReviewAsync(Review review)
        {
            var user = await userManager.FindByIdAsync(review.UserId);
            if (user == null) return (null, "UnAuthorized");

            var ExistReview = await reviewRepository.GetTableAsTracking().Include(r => r.Product)
                                                                         .Include(r => r.applicationUser)
                                                                         .FirstOrDefaultAsync(r => r.Id == review.Id);

            if (ExistReview == null)
                return (null, "NotFound");

            ExistReview.Comment = review.Comment;
            ExistReview.Rate = review.Rate;
            ExistReview.dateTime = DateTime.Now;

            using var Trans = await reviewRepository.BeginTransactionAsync();
            try
            {
                await reviewRepository.UpdateAsync(ExistReview);
                await Trans.CommitAsync();
                return (ExistReview, null);
            }
            catch (Exception ex)
            {
                await Trans.RollbackAsync();
                return (null, ex.Message);
            }
        }


        public async Task<(List<Review>?, string?)> GetProductReviewsAsync(int productId)
        {
            var IsProductExist = productRepository.GetTableNoTracking()
                .Any(p => p.Id == productId);

            if (!IsProductExist) return (null, "NotFound");
            try
            {
                var reviews = reviewRepository.GetTableNoTracking().Include(p => p.Product)
                                                              .Include(p => p.applicationUser)
                                                              .Where(r => r.ProductId == productId);

                if (reviews.IsNullOrEmpty()) return (null, "Empty");

                var ReviewsList = await reviews.ToListAsync();

                return (ReviewsList, null);
            }
            catch(Exception ex)
            {
                return (null, ex.Message);
            }

           
        }

        public async Task<(List<Review>?, string?)> GetUserReviewsAsync(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null) 
                return (null, "UnAuthorized");

            var reviews = reviewRepository.GetTableNoTracking().Include(p => p.Product)
                                                               .Include(p => p.applicationUser)
                                                               .Where(r => r.UserId == UserId);

            if (reviews.IsNullOrEmpty()) return (null, "Empty");

            var ReviewsList = await reviews.ToListAsync();

            return (ReviewsList, null);
        }

        
    }
}
