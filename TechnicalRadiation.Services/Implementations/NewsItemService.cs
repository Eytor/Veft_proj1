using System;
using System.Collections.Generic;
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
            var news = _newsItemRepository.GetAllNewsItems();
            foreach (var newsItem in news)
            {
                Link generalLink = new Link { href = $"api/{newsItem.Id}" };
                // Generate Links for all authors on this object
                var authorIds = _newsItemRepository.GetAllAuthorIdsByNewsId(newsItem.Id);
                List<Link> authorLinks = new List<Link>();
                foreach (var authorId in authorIds)
                {
                    authorLinks.Add(new Link { href = $"api/authors/{authorId}" });
                }
                // Generate links for all categories for this news story
                var categoryIds = _newsItemRepository.GetAllCategoryIdsByNewsId(newsItem.Id);
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
            }
            return new Envelope<NewsItemDto>(pageNumber, pageSize == 0 ? 25 : pageSize, news);
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
            throw new System.NotImplementedException();
        }
    }
}