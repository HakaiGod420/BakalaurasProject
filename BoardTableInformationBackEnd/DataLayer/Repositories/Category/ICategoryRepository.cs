using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Category
{
    public interface ICategoryRepository
    {
        public Task<CategoryEntity?> GetCategory(string categoryName);
        public Task<CategoryEntity> CreateCategory(CategoryEntity category);
    }
}
