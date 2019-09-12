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
    public class NewsItemRepository : INewsItemRepository
    {

        private static readonly string _admin = "TechnicalRadiationAdmin";
        public IEnumerable<NewsItemDto> GetAllNewsItems()
        {
             return DataProvider.NewsItems.Select(r =>
             {
                 return new NewsItemDto
                 {
                     Id = r.Id,
                     Title = r.Title,
                     ImgSource = r.ImgSource,
                     ShortDescription = r.ShortDescription
                 };
             });
        }

        public NewsItemDetailDto GetNewsById(int id)
        {
            var news = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            return new NewsItemDetailDto
            {
                Id = news.Id,
                Title = news.Title,
                ImgSource = news.ImgSource,
                ShortDescription = news.ShortDescription,
                LongDescription = news.LongDescription,
                PublishDate = news.PublishDate
            };
        }

        public int CreateNewNewsItem(NewsItemInputModel news)
        {
            var nextId = DataProvider.NewsItems.OrderByDescending(r => r.Id).FirstOrDefault().Id + 1; // Get next ID, quick fix according to Arnar.
            var entity = new NewsItem
            {
                Id = nextId,
                Title = news.Title,
                ImgSource = news.ImgSource,
                ShortDescription = news.ShortDescription,
                LongDescription = news.LongDescription,
                PublishDate = DateTime.Now,
                ModifiedBy = _admin,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };
            DataProvider.NewsItems.Add(entity);
            return nextId;
        }

        public void UpdateNewsItemById(NewsItemInputModel news, int id)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r => r.Id == id);
            if (entity == null) { return; }

            if (!String.IsNullOrEmpty(news.Title)) { entity.Title = news.Title; };
            if (!String.IsNullOrEmpty(news.ImgSource)) { entity.ImgSource = news.ImgSource; };
            if (!String.IsNullOrEmpty(news.ShortDescription)) { entity.ShortDescription = news.ShortDescription; };
            if (!String.IsNullOrEmpty(news.LongDescription)) { entity.LongDescription = news.LongDescription; };
            if (news.PublishDate != null) { entity.PublishDate = news.PublishDate; };
            entity.ModifiedBy = _admin;
            entity.ModifiedDate = DateTime.Now;
        }

        public void DeleteNewsById(int id)
        {
            var entity = DataProvider.NewsItems.FirstOrDefault(r=>r.Id == id);
            DataProvider.NewsItems.Remove(entity);
        }

        public IEnumerable<NewsItemDto> GetAllNewsItems(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> GetAllAuthorIdsByNewsId(int newsId) => DataProvider.NewsItemAuthors.Where(s => s.NewsItemId == newsId).Select(s => s.AuthorId);

        public IEnumerable<int> GetAllCategoryIdsByNewsId(int newsId) => DataProvider.NewsItemCategories.Where(s => s.NewsItemId == newsId).Select(s => s.CategoryId);

        public bool NewsItemExists(int newsItemId) =>  DataProvider.NewsItems.FirstOrDefault(r=>r.Id == newsItemId) != null;

    }
}