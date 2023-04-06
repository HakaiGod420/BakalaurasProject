using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Category;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class SelectListService : ISelectListService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBoardTypeRepository _boardTypeRepository;

        public SelectListService(ICategoryRepository categoryRepository, IBoardTypeRepository boardTypeRepository)
        {
            _categoryRepository = categoryRepository;
            _boardTypeRepository = boardTypeRepository;
        }

        public async Task<TypesAndCategoriesDto> GetTypesAndCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            var types = await _boardTypeRepository.GetTypes();

            return new TypesAndCategoriesDto
            {
                Categories = categories,
                Types = types
            };
        }
    }
}
