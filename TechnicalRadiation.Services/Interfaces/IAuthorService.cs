using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Services.Interfaces
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDetailDto GetAuthorById(int id);
        IEnumerable<NewsItemDto> GetNewsByAuthorId(int id);
        int CreateNewAuthor(AuthorInputModel author);
        void UpdateAuthorById(AuthorInputModel author, int id);
        void DeleteAuthorById(int id);
        void LinkAuthorToNewsItem(int authorId, int newsItemId);
    }
}