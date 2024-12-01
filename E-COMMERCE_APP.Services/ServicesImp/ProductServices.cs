using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Data.Enums;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using OneOf;




namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<string> AddProductAsync(Product product)
        {
            if (product == null) return "NotFound";

            // Check if a product with the same Name already exists
            var existingProduct = await productRepository
                .GetTableAsTracking()
                .FirstOrDefaultAsync(p => p.Name == product.Name);

            if (existingProduct != null)
                return "Exist";

            await productRepository.AddAsync(product);
            return "Success";
        }

        public async Task<string> DeleteProductAsync(int Id)
        {
            var trans = productRepository.BeginTransaction();

            try
            {
                var product = await productRepository.GetByIdAsync(Id);
                if (product == null)
                    return "NotFound";

                await productRepository.DeleteAsync(product);

                trans.Commit();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                Console.WriteLine($"Failed: {ex.Message}");
                return "Failed";
            }
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            var trans = productRepository.BeginTransaction();

            try
            {

                var existProduct = productRepository.GetTableNoTracking().FirstOrDefault(p => p.Id == product.Id);
                if (existProduct == null) return "NotFound";

                if (product.Equals(existProduct)) return "NoUpdates";

                await productRepository.UpdateAsync(product);
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                Console.WriteLine($"Error during product update: {ex.Message}");
                return "Failed";
            }
        }


        public IQueryable<Product> GetProductsByCategoryAsync(int categoryId, ProductFilterationEnum filter)
        {
            var products = productRepository.GetTableNoTracking()
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .AsQueryable();

            products = filter switch
            {
                ProductFilterationEnum.None => products,
                ProductFilterationEnum.FilterByName => products.OrderBy(p => p.Name),
                ProductFilterationEnum.FilterById => products.OrderBy(p => p.Id),
                ProductFilterationEnum.FilterByPriceAsc => products.OrderBy(p => p.Price),
                ProductFilterationEnum.FilterByPriceDes => products.OrderByDescending(p => p.Price),
                _ => products
            };

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int Id)
        {
            var product = await productRepository.GetTableNoTracking()
                .Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Id == Id);

            return product;
        }

        public async Task<Product> GetProductByNameAsync(string Name)
        {
            var product = await productRepository.GetTableNoTracking()
                .Include(p => p.Images)
                .SingleOrDefaultAsync(p => p.Name == Name);

            return product;
        }
    }
}
