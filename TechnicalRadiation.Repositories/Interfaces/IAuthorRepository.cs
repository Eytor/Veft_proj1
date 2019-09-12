using System.Collections.Generic;
using TechnicalRadiation.Models.Dtos;
using TechnicalRadiation.Models.InputModels;

namespace TechnicalRadiation.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        IEnumerable<AuthorDto> GetAllAuthors();
        AuthorDetailDto GetAuthorById(int id);
        IEnumerable<NewsItemDto> GetNewsByAuthorId(int id);
        int CreateNewAuthor(AuthorInputModel author);
        void UpdateAuthorById(AuthorInputModel author, int id);
        void DeleteAuthorById(int id);
        void LinkAuthorToNewsItem(int authorId, int newsItemId);
        IEnumerable<int> GetAllNewsItemIdsByAuthorId(int authorId);
        IEnumerable<int> GetAuthorIdsbyNewsItemId(int newsItemId);
        IEnumerable<int> GetCategoriesIdsbyNewsItemId(int newsItemId);
        bool AuthorExists(int authorId);
        bool NewsItemExists(int newsItemId);
    }
}