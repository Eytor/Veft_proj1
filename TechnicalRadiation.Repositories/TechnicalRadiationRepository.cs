using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Repositories
{
    public class TechnicalRadiationRepository
    {
        public IEnumerable<NewsItemDto> GetAllNewsItems(int pageSize)
        {
            return DataProvider.NewsItems.Take(pageSize > 0 ? pageSize : 25).Select(r => new NewsItemDto {
                Id = r.Id,
                Title = r.Title,
                ImgSource = r.ImgSource,
                ShortDescription = r.ShortDescription
            });
        }
        public NewsItemDetailDto GetNewsById(int id)
        {
            var news = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            if (news == null)
            {
                return null;
            }
            return new NewsItemDetailDto {
                Id = news.Id,
                Title = news.Title,
                ImgSource = news.ImgSource,
                ShortDescription = news.ShortDescription,
                LongDescription = news.LongDescription,
                PublishDate = news.PublishDate
            };
        }
    }
}