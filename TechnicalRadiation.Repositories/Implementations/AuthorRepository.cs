using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.Entities;
using TechnicalRadiation.Models.InputModels;
using TechnicalRadiation.Repositories.Data;
using TechnicalRadiation.Repositories.Interfaces;

namespace TechnicalRadiation.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private static readonly string _admin = "TechnicalRadiationAdmin";
        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return DataProvider.Authors.Select(r =>
            {
                return new AuthorDto
                {
                    Id = r.Id,
                    Name = r.Name
                };
            });
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            var author = DataProvider.Authors.FirstOrDefault(x => x.Id == id);

            return new AuthorDetailDto
            {
                Id = author.Id,
                Name = author.Name,
                ProfileImgSource = author.ProfileImgSource,
                Bio = author.Bio
            };
        }

        public int CreateNewAuthor(AuthorInputModel author)
        {
            var nextId = DataProvider.Authors.OrderByDescending(r => r.Id).FirstOrDefault().Id + 1; // Get next ID, quick fix according to Arnar.
            var entity = new Author
            {
                Id = nextId,
                Name = author.Name,
                ProfileImgSource = author.ProfileImgSource,
                Bio = author.Bio,
                ModifiedBy = _admin,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            DataProvider.Authors.Add(entity);
            return nextId;
        }

        public void UpdateAuthorById(AuthorInputModel author, int id)
        {
            var entity = DataProvider.Authors.FirstOrDefault(r => r.Id == id);

            if (!String.IsNullOrEmpty(author.Name)) { entity.Name = author.Name; };
            if (!String.IsNullOrEmpty(author.ProfileImgSource)) { entity.ProfileImgSource = author.ProfileImgSource; };
            if (!String.IsNullOrEmpty(author.Bio)) { entity.Bio = author.Bio; };
            entity.ModifiedBy = _admin;
            entity.ModifiedDate = DateTime.Now;
        }

        public void DeleteAuthorById(int id)
        {
            var entity = DataProvider.Authors.FirstOrDefault(r=>r.Id == id);
            DataProvider.Authors.Remove(entity);
        }

        public void LinkAuthorToNewsItem(int authorId, int newsItemId)
        {
            var entity = new NewsItemAuthors
            {
                AuthorId = authorId,
                NewsItemId = newsItemId
            };
            DataProvider.NewsItemAuthors.Add(entity);
        }
        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int id)
        {
            return DataProvider.NewsItems.Join(DataProvider.NewsItemAuthors, NewsItemDto => NewsItemDto.Id, NewsItemAuthors => NewsItemAuthors.NewsItemId,
            (NewsItemDto, NewsItemAuthors) => new { NewsItem = NewsItemDto, NewsItemAuthors = NewsItemAuthors })
            .Where(r => r.NewsItemAuthors.AuthorId == id).Select(r =>
            {
                return new NewsItemDto
                {
                    Id = r.NewsItem.Id,
                    Title = r.NewsItem.Title,
                    ImgSource = r.NewsItem.ImgSource,
                    ShortDescription = r.NewsItem.ShortDescription
                };
            });
        }

        public IEnumerable<int> GetAllNewsItemIdsByAuthorId(int authorId) => DataProvider.NewsItemAuthors.Where(s => s.AuthorId == authorId).Select(s => s.NewsItemId);
        public IEnumerable<int> GetAuthorIdsbyNewsItemId(int newsItemId) => DataProvider.NewsItemAuthors.Where(s => s.NewsItemId == newsItemId).Select(s => s.AuthorId);
        public IEnumerable<int> GetCategoriesIdsbyNewsItemId(int newsItemId) => DataProvider.NewsItemCategories.Where(s => s.NewsItemId == newsItemId).Select(s => s.CategoryId);

        public bool AuthorExists(int authorId) => DataProvider.Authors.FirstOrDefault(r=>r.Id == authorId) != null;
        public bool NewsItemExists(int newsItemId) => DataProvider.NewsItems.FirstOrDefault(r=>r.Id == newsItemId) != null;
    }
}