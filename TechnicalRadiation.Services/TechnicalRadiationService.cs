using System;
using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class TechnicalRadiationService
    {
        private TechnicalRadiationRepository _technicalRadiationRepo = new TechnicalRadiationRepository();

        public IEnumerable<NewsItemDto> GetAllNews(int pageSize)
        {
            return _technicalRadiationRepo.GetAllNewsItems(pageSize);
        }

        public NewsItemDetailDto GetNewsById(int id)
        {
            return _technicalRadiationRepo.GetNewsById(id);
        }
    }
}