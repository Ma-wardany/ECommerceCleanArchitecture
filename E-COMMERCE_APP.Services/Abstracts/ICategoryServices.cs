using E_COMMERCE_APP.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_COMMERCE_APP.Services.Abstracts
{
    public interface ICategoryServices
    {
        public Task<string> AddCategoryAsync(Category category);
        public Task<string> UpdateCategoryAsync(Category category);
        public Task<string> DeleteCategoryAsync(int Id);
    }
}
