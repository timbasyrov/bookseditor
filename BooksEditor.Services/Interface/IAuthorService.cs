using System.Collections.Generic;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public interface IAuthorService
    {
        AuthorModel GetAuthor(int id);
        IEnumerable<AuthorModel> GetAuthorList();
        ActionResultModel AddAuthor(AuthorModel author);
        ActionResultModel UpdateAuthor(AuthorModel author);
        ActionResultModel DeleteAuthor(int id);
    }
}
