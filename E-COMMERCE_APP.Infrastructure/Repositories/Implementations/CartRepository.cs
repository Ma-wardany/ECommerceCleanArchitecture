using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Infrastructure.Context;
using E_COMMERCE_APP.Infrastructure.InfrastructureBases;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Infrastructure.Repositories.Implementations
{
    public class CartRepository : GenericRepositoryAsync<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
