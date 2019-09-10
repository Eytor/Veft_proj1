using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.Repositories
{
    public class TechnicalRadiationRepository
    {
        public Envelope<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber)
        {
            return new Envelope<NewsItemDto>(pageNumber, pageSize == 0 ? 25: pageSize, DataProvider.NewsItems.Select(r =>
            {
                var dto = new NewsItemDto {
                Id = r.Id,
                Title = r.Title,
                ImgSource = r.ImgSource,
                ShortDescription = r.ShortDescription
                };
                Link generalLink = new Link{ href = $"api/{dto.Id}" };
                // Generate Links for all authors on this object
                var authorIds = DataProvider.NewsItemAuthors.Where(s => s.NewsItemId == r.Id).Select(s => s.AuthorId);
                List<Link> authorLinks = new List<Link>();
                foreach (var authorId in authorIds)
                {
                    authorLinks.Add(new Link{ href = $"api/authors/{authorId}"});
                }
                // Generate links for all categories for this news story
                var categoryIds = DataProvider.NewsItemCategories.Where(s => s.NewsItemId == r.Id).Select(s => s.CategoryId);
                List<Link> categoryLinks = new List<Link>();
                foreach (var categoryId in categoryIds)
                {
                    categoryLinks.Add(new Link{ href = $"api/categories/{categoryId}"});
                }
                dto.Links.AddReference("self", generalLink);
                dto.Links.AddReference("edit", generalLink);
                dto.Links.AddReference("delete", generalLink);
                dto.Links.AddListReference("authors", authorLinks);
                dto.Links.AddListReference("categories", categoryLinks);
                return dto;
            }));
        }

        public NewsItemDetailDto GetNewsById(int id)
        {
            var news = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            if (news == null)
            {
                return null;
            }
            NewsItemDetailDto dto = new NewsItemDetailDto {
                Id = news.Id,
                Title = news.Title,
                ImgSource = news.ImgSource,
                ShortDescription = news.ShortDescription,
                LongDescription = news.LongDescription,
                PublishDate = news.PublishDate
            };
            Link generalLink = new Link{ href = $"api/{news.Id}" };
            // Generate Links for all authors on this object
            var authorIds = DataProvider.NewsItemAuthors.Where(s => s.NewsItemId == id).Select(s => s.AuthorId);
            List<Link> authorLinks = new List<Link>();
            foreach (var authorId in authorIds)
            {
                authorLinks.Add(new Link{ href = $"api/authors/{authorId}"});
            }
            // Generate links for all categories for this news story
            var categoryIds = DataProvider.NewsItemCategories.Where(s => s.NewsItemId == id).Select(s => s.CategoryId);
            List<Link> categoryLinks = new List<Link>();
            foreach (var categoryId in categoryIds)
            {
                categoryLinks.Add(new Link{ href = $"api/categories/{categoryId}"});
            }
            dto.Links.AddReference("self", generalLink);
            dto.Links.AddReference("edit", generalLink);
            dto.Links.AddReference("delete", generalLink);
            dto.Links.AddListReference("authors", authorLinks);
            dto.Links.AddListReference("categories", categoryLinks);
            return dto;
        }

        public IEnumerable<CategoryDto> getAllCategories()
        {
            return DataProvider.Categories.Select(r =>
            {
                var dto = new CategoryDto{
                Id = r.Id,
                Name = r.Name,
                Slug = r.Slug
                };

                Link generalLink = new Link{ href = $"api/categories/{r.Id}" };

                dto.Links.AddReference("self", generalLink);
                dto.Links.AddReference("edit", generalLink);
                dto.Links.AddReference("delete", generalLink);

                return dto;
            });
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            var cat = DataProvider.Categories.FirstOrDefault(r => r.Id == id);
            if (cat == null)
            {
                return null;
            }
            CategoryDetailDto dto = new CategoryDetailDto {
                Id = cat.Id,
                Name = cat.Name,
                Slug = cat.Slug,
                NumberOfNewsItems = DataProvider.NewsItemCategories.Count(r => r.CategoryId == cat.Id)
            };

            Link generalLink = new Link{ href = $"api/categories/{id}" };

            dto.Links.AddReference("self", generalLink);
            dto.Links.AddReference("edit", generalLink);
            dto.Links.AddReference("delete", generalLink);

            return dto;
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return DataProvider.Authors.Select(r =>
            {
                var dto = new AuthorDto{
                    Id = r.Id,
                    Name = r.Name
                };

                Link generalLink = new Link{ href = $"api/authors/{r.Id}" };
                Link newsItemsLink = new Link{ href = $"api/authors/{r.Id}/newsItems" };
                // Generate Links for all authors on this object
                var newsItemIds = DataProvider.NewsItemAuthors.Where(s => s.AuthorId == r.Id).Select(s => s.NewsItemId);
                List<Link> NewsItemDetailedLinks = new List<Link>();
                foreach (var newsItemId in newsItemIds)
                {
                    NewsItemDetailedLinks.Add(new Link{ href = $"api/{newsItemId}"});
                }

                dto.Links.AddReference("self", generalLink);
                dto.Links.AddReference("edit", generalLink);
                dto.Links.AddReference("delete", generalLink);
                dto.Links.AddReference("newsItems", newsItemsLink);
                dto.Links.AddListReference("newsItemDetailed", NewsItemDetailedLinks);

                return dto;
            });
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = DataProvider.Authors.FirstOrDefault(x => x.Id == id);
            if (author == null)
            {
                return null;
            }
            AuthorDetailDto dto = new AuthorDetailDto {
                Id = author.Id,
                Name = author.Name,
                ProfileImgSource = author.ProfileImgSource,
                Bio = author.Bio
            };

            Link generalLink = new Link{ href = $"api/authors/{id}" };
            Link newsItemLink = new Link{ href = $"api/authors/{id}/newsItems" };

            var newsItemIds = DataProvider.NewsItemAuthors.Where(s => s.AuthorId == id).Select(s => s.NewsItemId);
            List<Link> NewsItemDetailedLinks = new List<Link>();
            foreach (var newsItemId in newsItemIds)
            {
                NewsItemDetailedLinks.Add(new Link{ href = $"api/{newsItemId}"});
            }


            dto.Links.AddReference("self", generalLink);
            dto.Links.AddReference("edit", generalLink);
            dto.Links.AddReference("delete", generalLink);
            dto.Links.AddReference("newsItems", newsItemLink);
            dto.Links.AddListReference("newsItemsDetailed", NewsItemDetailedLinks);

            return dto;
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int id)
        {
            return DataProvider.NewsItems.Join(DataProvider.NewsItemAuthors, NewsItemDto => NewsItemDto.Id, NewsItemAuthors => NewsItemAuthors.NewsItemId,
            ( NewsItemDto, NewsItemAuthors ) => new {NewsItem = NewsItemDto, NewsItemAuthors = NewsItemAuthors} )
            .Where(r => r.NewsItemAuthors.AuthorId == id).Select(r =>
            {
                var dto = new NewsItemDto {
                Id = r.NewsItem.Id,
                Title = r.NewsItem.Title,
                ImgSource = r.NewsItem.ImgSource,
                ShortDescription = r.NewsItem.ShortDescription
                };
                Link generalLink = new Link{ href = $"api/{dto.Id}" };
                // Generate Links for all authors on this object
                var authorIds = DataProvider.NewsItemAuthors.Where(s => s.NewsItemId == r.NewsItem.Id).Select(s => s.AuthorId);
                List<Link> authorLinks = new List<Link>();
                foreach (var authorId in authorIds)
                {
                    authorLinks.Add(new Link{ href = $"api/authors/{authorId}"});
                }
                // Generate links for all categories for this news story
                var categoryIds = DataProvider.NewsItemCategories.Where(s => s.NewsItemId == r.NewsItem.Id).Select(s => s.CategoryId);
                List<Link> categoryLinks = new List<Link>();
                foreach (var categoryId in categoryIds)
                {
                    categoryLinks.Add(new Link{ href = $"api/categories/{categoryId}"});
                }
                dto.Links.AddReference("self", generalLink);
                dto.Links.AddReference("edit", generalLink);
                dto.Links.AddReference("delete", generalLink);
                dto.Links.AddListReference("authors", authorLinks);
                dto.Links.AddListReference("categories", categoryLinks);
                return dto;
            });
        }

        public IEnumerable<NewsItemDto> CreateNews(NewsItem news)
        {
            var nextId = DataProvider.NewsItems.OrderByDescending(r => r.Id).FirstOrDefault().Id + 1; // Get next ID, quick fix according to Arnar.
            var entity = new NewsItem{
            Id = nextId,
            Title = news.Title,
            ImgSource = news.ImgSource,
            ShortDescription = news.ShortDescription,
            LongDescription = news.LongDescription,
            PublishDate = DateTime.Now,
            ModifiedBy = "Admin",
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
            };
            DataProvider.NewsItems.Add(entity);
            return new NewsItemDto{
            Id = nextId,
            Title = entity.Title,
            ImgSource = entity.ImgSource,
            ShortDescription = entity.ShortDescription,
            LongDescription = entity.LongDescription,

            };
        }

        public IEnumerable<NewsItemDto> CreateNewsItemId(int id)
        {
            return _technicalRadiationRepo.CreateNewsItemId(id);
        }

        public IEnumerable<NewsItemDto> DeleteNewsItemId(int id)
        {
            return TechnicalRadiationRepository.DeleteNewsItemId(id);
        }

        public IEnumerable<NewsItemDto> CreateCategories()
        {
            return _technicalRadiationRepo.CreateCategories();
        }
    }
}