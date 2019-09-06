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

        public IEnumerable<CategoryDto> getAllCategories()
        {
            return DataProvider.Categories.Select(r => new CategoryDto{
                Id = r.Id,
                Name = r.Name ,
                Slug = r.Slug
            });
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            var cat = DataProvider.Categories.FirstOrDefault(r => r.Id == id);
            if (cat == null)
            {
                return null;
            }
            return new CategoryDetailDto {
                Id = cat.Id,
                Name = cat.Name,
                Slug = cat.Slug,
                NumberOfNewsItems = DataProvider.NewsItemCategories.Count(r => r.CategoryId == cat.Id)
            };
        }
    }
}