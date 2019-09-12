using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface ICategoryService
    {
         IEnumerable<CategoryDto> GetAllCategories();
         CategoryDetailDto GetCategoryById(int id);
         int CreateNewCategory(CategoryInputModel category);
         void UpdateCategoryById(CategoryInputModel category, int id);
         void DeleteCategoryById(int id);
         void LinkNewsItemToCategory(int categoryId, int newsItemId);
    }
}