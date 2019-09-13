using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return _categoryRepository.getAllCategories().Select(category =>
            {
                // Adding links to categories
                Link generalLink = new Link { href = $"api/categories/{category.Id}" };

                category.Links.AddReference("self", generalLink);
                category.Links.AddReference("edit", generalLink);
                category.Links.AddReference("delete", generalLink);
                return category;
            });
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            if (!_categoryRepository.CategoryExists(id)) { return null; };
            var category = _categoryRepository.GetCategoryById(id);
            category.NumberOfNewsItems = _categoryRepository.GetNumberOfNewsItemsInCategory(id);

            Link generalLink = new Link { href = $"api/categories/{id}" };

            category.Links.AddReference("self", generalLink);
            category.Links.AddReference("edit", generalLink);
            category.Links.AddReference("delete", generalLink);
            return category;
        }

        public int CreateNewCategory(CategoryInputModel category)
        {
            var slug = String.Join("-", category.Name.ToLower().Split(' ')); // Generating slug from name in lowecase and joined by a hyphen
            return _categoryRepository.CreateNewCategory(category, slug);
        }

        public void UpdateCategoryById(CategoryInputModel category, int id)
        {
            if (!_categoryRepository.CategoryExists(id)) { throw new Exception($"category not found"); }
            var slug = String.Join("-", category.Name.ToLower().Split(' ')); // Generating slug from name in lowecase and joined by a hyphen
            _categoryRepository.UpdateCategoryById(category, id, slug);
        }

        public void DeleteCategoryById(int id)
        {
            if (!_categoryRepository.CategoryExists(id)) { throw new Exception($"category not found"); };
            _categoryRepository.DeleteCategoryById(id);
        }

        public void LinkNewsItemToCategory(int categoryId, int newsItemId)
        {
            if (!_categoryRepository.CategoryExists(categoryId)) { throw new Exception($"category not found"); };
            if (!_categoryRepository.NewsItemExists(newsItemId)) { throw new Exception($"news item not found"); };
            _categoryRepository.LinkNewsItemToCategory(categoryId, newsItemId);
        }
    }
}