using DataLayer.DBContext;
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

        public async Task<bool> CheckIfExists(string categoryName)
        {
            return await _dbContext.Categories.AnyAsync(c => c.CategoryName == categoryName);
        }
    }
}
