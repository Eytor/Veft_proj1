using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {

        private static readonly string _admin = "TechnicalRadiationAdmin";
        public IEnumerable<CategoryDto> getAllCategories()
        {
            return DataProvider.Categories.Select(r =>
            {
                return new CategoryDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Slug = r.Slug
                };
            });
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            var cat = DataProvider.Categories.FirstOrDefault(r => r.Id == id);

            CategoryDetailDto dto = new CategoryDetailDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Slug = cat.Slug
            };

            return dto;
        }

        public int CreateNewCategory(CategoryInputModel category, string slug)
        {
            var nextId = DataProvider.Categories.OrderByDescending(r => r.Id).FirstOrDefault().Id + 1; // Get next ID, quick fix according to Arnar.
            var entity = new Category
            {
                Id = nextId,
                Name = category.Name,
                Slug = slug,
                ModifiedBy = _admin,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            DataProvider.Categories.Add(entity);
            return nextId;
        }

        public void UpdateCategoryById(CategoryInputModel category, int id, string slug)
        {
            var entity = DataProvider.Categories.FirstOrDefault(r => r.Id == id);

            if (!String.IsNullOrEmpty(category.Name))
            {
                entity.Name = category.Name;
                entity.Slug = slug;
            };
            entity.ModifiedBy = _admin;
            entity.ModifiedDate = DateTime.Now;
        }

        public void DeleteCategoryById(int id)
        {
            var entity = DataProvider.Categories.FirstOrDefault(r=>r.Id == id);
            DataProvider.Categories.Remove(entity);
        }
        public void LinkNewsItemToCategory(int categoryId, int newsItemId)
        {
            var entity = new NewsItemCategories
            {
                CategoryId = categoryId,
                NewsItemId = newsItemId
            };
            DataProvider.NewsItemCategories.Add(entity);
        }

        public int GetNumberOfNewsItemsInCategory(int categoryId) => DataProvider.NewsItemCategories.Count(r => r.CategoryId == categoryId);
        public bool CategoryExists(int categoryId) => DataProvider.Categories.FirstOrDefault(r=>r.Id == categoryId) != null;

        public bool NewsItemExists(int newsItemId) => DataProvider.NewsItems.FirstOrDefault(r=>r.Id == newsItemId) != null;

    }
}