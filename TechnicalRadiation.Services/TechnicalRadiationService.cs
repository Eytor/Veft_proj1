using System;
using System.Collections.Generic;
using TechnicalRadiation.Models;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class TechnicalRadiationService
    {
        private TechnicalRadiationRepository _technicalRadiationRepo = new TechnicalRadiationRepository();

        public Envelope<NewsItemDto> GetAllNews(int pageSize, int pageNumber)
        {
            return _technicalRadiationRepo.GetAllNewsItems(pageSize, pageNumber);
        }

        public NewsItemDetailDto GetNewsById(int id)
        {
            return _technicalRadiationRepo.GetNewsById(id);
        }

        public IEnumerable<CategoryDto> getAllCategories()
        {
            return _technicalRadiationRepo.getAllCategories();
        }

        public CategoryDetailDto GetCategoryById(int id)
        {
            return _technicalRadiationRepo.GetCategoryById(id);
        }

        public IEnumerable<AuthorDto> GetAllAuthors()
        {
            return _technicalRadiationRepo.GetAllAuthors();
        }

        public AuthorDetailDto GetAuthorById(int id)
        {
            return _technicalRadiationRepo.GetAuthorById(id);
        }

        public IEnumerable<NewsItemDto> GetNewsByAuthorId(int id)
        {
            return _technicalRadiationRepo.GetNewsByAuthorId(id);
        }
    }
}