using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Infrastructure.InfrastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Infrastructure.Repositories.Abstracts
{
    public interface IOrderRepository : IGenericRepositoryAsync<Order>
    {
    }
}
