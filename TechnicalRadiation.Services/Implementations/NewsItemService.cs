using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Interfaces;
using TechnicalRadiation.Services.Interfaces;

namespace TechnicalRadiation.Services.Implementations
{
    public class NewsItemService : INewsItemService
    {
        private readonly INewsItemRepository _newsItemRepository;

        public NewsItemService(INewsItemRepository categoryRepository) => _newsItemRepository = categoryRepository;

        public Envelope<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber)
        {
            return new Envelope<NewsItemDto>(pageNumber, pageSize == 0 ? 25 : pageSize, _newsItemRepository.GetAllNewsItems().Select(r =>
            {
                Link generalLink = new Link { href = $"api/{r.Id}" };
                // Generate Links for all authors on this object
                var authorIds = _newsItemRepository.GetAllAuthorIdsByNewsId(r.Id);
                List<Link> authorLinks = new List<Link>();
                foreach (var authorId in authorIds)
                {
                    authorLinks.Add(new Link { href = $"api/authors/{authorId}" });
                }

                // Generate links for all categories for this news story
                var categoryIds = _newsItemRepository.GetAllCategoryIdsByNewsId(r.Id);
                List<Link> categoryLinks = new List<Link>();
                foreach (var categoryId in categoryIds)
                {
                    categoryLinks.Add(new Link { href = $"api/categories/{categoryId}" });
                }

                r.Links.AddReference("self", generalLink);
                r.Links.AddReference("edit", generalLink);
                r.Links.AddReference("delete", generalLink);
                r.Links.AddListReference("authors", authorLinks);
                r.Links.AddListReference("categories", categoryLinks);
                return r;
            }));
        }

        public NewsItemDetailDto GetNewsById(int id)
        {
            if (!_newsItemRepository.NewsItemExists(id)) { return null; };
            var newsItem = _newsItemRepository.GetNewsById(id);

            Link generalLink = new Link { href = $"api/{id}" };
            // Generate Links for all authors on this object
            var authorIds = _newsItemRepository.GetAllAuthorIdsByNewsId(id);
            List<Link> authorLinks = new List<Link>();
            foreach (var authorId in authorIds)
            {
                authorLinks.Add(new Link { href = $"api/authors/{authorId}" });
            }
            // Generate links for all categories for this news story
            var categoryIds = _newsItemRepository.GetAllCategoryIdsByNewsId(id);
            List<Link> categoryLinks = new List<Link>();
            foreach (var categoryId in categoryIds)
            {
                categoryLinks.Add(new Link { href = $"api/categories/{categoryId}" });
            }
            newsItem.Links.AddReference("self", generalLink);
            newsItem.Links.AddReference("edit", generalLink);
            newsItem.Links.AddReference("delete", generalLink);
            newsItem.Links.AddListReference("authors", authorLinks);
            newsItem.Links.AddListReference("categories", categoryLinks);
            return newsItem;

        }

        public int CreateNewNewsItem(NewsItemInputModel news)
        {
            return _newsItemRepository.CreateNewNewsItem(news);
        }

        public void UpdateNewsItemById(NewsItemInputModel news, int id)
        {
            if (!_newsItemRepository.NewsItemExists(id)) { throw new Exception($"category not found"); }
            _newsItemRepository.UpdateNewsItemById(news, id);
        }

        public void DeleteNewsById(int id)
        {
            if (!_newsItemRepository.NewsItemExists(id)) { throw new Exception($"category not found"); }
            _newsItemRepository.DeleteNewsById(id);
        }
    }
}