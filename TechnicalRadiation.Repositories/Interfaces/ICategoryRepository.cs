using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryDto> getAllCategories();
        CategoryDetailDto GetCategoryById(int id);
        int CreateNewCategory(CategoryInputModel category, string slug);
        void UpdateCategoryById(CategoryInputModel category, int id, string slug);
        void DeleteCategoryById(int id);
        void LinkNewsItemToCategory(int categoryId, int newsItemId);
        int GetNumberOfNewsItemsInCategory(int categoryId);
        bool CategoryExists(int categoryId);
        bool NewsItemExists(int newsItemId);
    }
}