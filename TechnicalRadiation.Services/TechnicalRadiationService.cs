using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Repositories;

namespace TechnicalRadiation.Services
{
    public class TechnicalRadiationService
    {
        private TechnicalRadiationRepository _technicalRadiationRepo = new TechnicalRadiationRepository();

        public IEnumerable<NewsItemDto> GetAllNews()
        {
            return _technicalRadiationRepo.GetAllNewsItems();
        }
    }
}