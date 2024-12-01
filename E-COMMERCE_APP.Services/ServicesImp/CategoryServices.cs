using E_COMMERCE_APP.Data.Entities;
using E_COMMERCE_APP.Infrastructure.Repositories.Abstracts;
using E_COMMERCE_APP.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.ServicesImp
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<string> AddCategoryAsync(Category category)
        {
            if (category == null) return "NotFound";

            var Trans = categoryRepository.BeginTransaction();
            try
            {
                var categories = categoryRepository.GetTableNoTracking();

                // check if category is already exist
                var isExist = await categories
                    .AnyAsync(c => c.Id == category.Id || c.Name == category.Name);

                if (isExist)
                    return "Exist";

                await categoryRepository.AddAsync(category);

                Trans.Commit();
                return "Success";
            }
            catch (Exception ex)
            {
                Trans.Rollback();
                Console.WriteLine($"Error : {ex.Message} ---------------------------");
                return "Failed";
            }
           
        }

        public async Task<string> DeleteCategoryAsync(int id)
        {
            // Check if the category exists
            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
                return "NotFound";

            // Use a transaction for the delete operation
            using var transaction = categoryRepository.BeginTransaction();
            try
            {
                await categoryRepository.DeleteAsync(category);
                transaction.Commit();

                return "Success";
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine($"Error : {e.Message} ----------------------");
                return "Failed";
            }
        }


        public async Task<string> UpdateCategoryAsync(Category category)
        {

            var existingCategory = await categoryRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory == null)
                return "NotFound";

            if (existingCategory.Name == category.Name &&
                existingCategory.Description == category.Description)
            {
                return "NoUpdates";
            }

            // Use a transaction for the update operation
            using var transaction = categoryRepository.BeginTransaction();
            try
            {
                await categoryRepository.UpdateAsync(category);
                transaction.Commit();

                return "Success";
            }
            catch (Exception e)
            {
                transaction.Rollback();
                Console.WriteLine($"Error updating category: {e.Message}");
                return "Failed";
            }
        }

    }
}
