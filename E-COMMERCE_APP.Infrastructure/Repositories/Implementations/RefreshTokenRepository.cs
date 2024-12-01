using E_COMMERCE_APP.Data.Entities.Identity;
using E_COMMERCE_APP.Infrastructure.Context;
using E_COMMERCE_APP.Infrastructure.InfrastructureBases;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_COMMERCE_APP.Infrastructure.Context;

namespace E_COMMERCE_APP.Infrastructure.Repositories.Implementations
{
    internal class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
