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
    }
}