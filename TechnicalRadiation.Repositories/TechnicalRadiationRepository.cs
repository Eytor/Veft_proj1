using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Repositories
{
    public class TechnicalRadiationRepository
    {
        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
            return DataProvider.NewsItems.Select(r => new NewsItemDto {
                Id = r.Id,
                Title = r.Title,
                ImgSource = r.ImgSource,
                ShortDescription = r.ShortDescription
            });
        }
    }
}