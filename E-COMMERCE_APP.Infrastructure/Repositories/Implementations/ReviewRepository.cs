using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Infrastructure.Context;
using E_COMMERCE_APP.Infrastructure.InfrastructureBases;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;

namespace E_COMMERCE_APP.Infrastructure.Repositories.Implementations
{
    public class ReviewRepository : GenericRepositoryAsync<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context) { }

    }
}
