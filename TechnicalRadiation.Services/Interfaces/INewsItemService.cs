using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface INewsItemService
    {
         Envelope<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber);
         NewsItemDetailDto GetNewsById(int id);
         int CreateNewNewsItem(NewsItemInputModel news);
         void UpdateNewsItemById(NewsItemInputModel news, int id);
         void DeleteNewsById(int id);
    }
}