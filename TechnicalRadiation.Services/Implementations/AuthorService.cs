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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository) => _authorRepository = authorRepository;

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            foreach (var author in authors)
            {
                Link generalLink = new Link { href = $"api/authors/{author.Id}" };
                Link newsItemsLink = new Link { href = $"api/authors/{author.Id}/newsItems" };

                // Generate Links for all authors on this object
                var newsItemIds = _authorRepository.GetAllNewsItemIdsByAuthorId(author.Id);
                List<Link> NewsItemDetailedLinks = new List<Link>();
                foreach (var newsItemId in newsItemIds)
                {
                    NewsItemDetailedLinks.Add(new Link { href = $"api/{newsItemId}" });
                }

                author.Links.AddReference("self", generalLink);
                author.Links.AddReference("edit", generalLink);
                author.Links.AddReference("delete", generalLink);
                author.Links.AddReference("newsItems", newsItemsLink);
                author.Links.AddListReference("newsItemDetailed", NewsItemDetailedLinks);
            }
            return authors;
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            if (!_authorRepository.AuthorExists(id)) { return null; };
            var author = _authorRepository.GetAuthorById(id);

            Link generalLink = new Link { href = $"api/authors/{id}" };
            Link newsItemLink = new Link { href = $"api/authors/{id}/newsItems" };

            var newsItemIds = _authorRepository.GetAllNewsItemIdsByAuthorId(id);
            List<Link> NewsItemDetailedLinks = new List<Link>();
            foreach (var newsItemId in newsItemIds)
            {
                NewsItemDetailedLinks.Add(new Link { href = $"api/{newsItemId}" });
            }


            author.Links.AddReference("self", generalLink);
            author.Links.AddReference("edit", generalLink);
            author.Links.AddReference("delete", generalLink);
            author.Links.AddReference("newsItems", newsItemLink);
            author.Links.AddListReference("newsItemsDetailed", NewsItemDetailedLinks);

            return author;
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int id)
        {
            var news =_authorRepository.GetNewsByAuthorId(id);
            foreach (var newsItem in news)
            {
                Link generalLink = new Link { href = $"api/{newsItem.Id}" };
                // Generate Links for all authors on this object
                var authorIds = _authorRepository.GetAuthorIdsbyNewsItemId(newsItem.Id);
                List<Link> authorLinks = new List<Link>();
                foreach (var authorId in authorIds)
                {
                    authorLinks.Add(new Link { href = $"api/authors/{authorId}" });
                }
                // Generate links for all categories for this news story
                var categoryIds = _authorRepository.GetCategoriesIdsbyNewsItemId(newsItem.Id);
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
            return news;
        }

        public int CreateNewAuthor(AuthorInputModel author)
        {
            return _authorRepository.CreateNewAuthor(author);
        }

        public void UpdateAuthorById(AuthorInputModel author, int id)
        {
            if (!_authorRepository.AuthorExists(id)) { throw new Exception($"author not found"); };
            _authorRepository.UpdateAuthorById(author, id);
        }

        public void DeleteAuthorById(int id)
        {
            if (!_authorRepository.AuthorExists(id)) { throw new Exception($"author not found"); };
            _authorRepository.DeleteAuthorById(id);
        }

        public void LinkAuthorToNewsItem(int authorId, int newsItemId)
        {
            if (!_authorRepository.AuthorExists(authorId)) { throw new Exception($"author not found"); };
            if (!_authorRepository.NewsItemExists(newsItemId)) { throw new Exception($"newsItem not found"); };
            _authorRepository.LinkAuthorToNewsItem(authorId, newsItemId);
        }
    }
}