using System.Collections.Generic;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface INewsItemRepository
    {
        IEnumerable<NewsItemDto> GetAllNewsItems();
        NewsItemDetailDto GetNewsById(int id);
        int CreateNewNewsItem(NewsItemInputModel news);
        void UpdateNewsItemById(NewsItemInputModel news, int id);
        void DeleteNewsById(int id);
        IEnumerable<int> GetAllAuthorIdsByNewsId(int newsId);
        IEnumerable<int> GetAllCategoryIdsByNewsId(int newsId);
        bool NewsItemExists(int newsItemId);
    }
}