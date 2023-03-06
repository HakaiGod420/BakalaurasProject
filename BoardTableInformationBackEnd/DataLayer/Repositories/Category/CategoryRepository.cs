using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataBaseContext _dbContext;

        public CategoryRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryEntity> CreateCategory(CategoryEntity category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryEntity?> GetCategory(string categoryName)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
        }
    }
}
