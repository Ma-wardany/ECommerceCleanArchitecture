using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IReviewServices
    {
        public Task<(Review?, string?)>       AddReviewAsync(Review review); 
        public Task<string>                   DeleteReviewAsync(string UserId, int reviewId); 
        public Task<(Review?, string?)>       UpdateReviewAsync(Review review);
        public Task<(List<Review>?, string?)> GetProductReviewsAsync(int productId);
        public Task<(List<Review>?, string?)> GetUserReviewsAsync(string UserId);
    }
}
