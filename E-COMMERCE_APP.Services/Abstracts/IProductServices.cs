using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Enums;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface IProductServices
    {
        public Task<string>        AddProductAsync(Product product);
        public Task<string>        UpdateProductAsync(Product product);
        public Task<string>        DeleteProductAsync(int Id);
        public IQueryable<Product> GetProductsByCategoryAsync(int categoryId, ProductFilterationEnum filter);
        public Task<Product>       GetProductByIdAsync(int Id);
        public Task<Product>       GetProductByNameAsync(string Name);
    }
}
