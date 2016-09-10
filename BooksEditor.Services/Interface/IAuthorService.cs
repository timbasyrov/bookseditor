using System.Collections.Generic;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public interface IAuthorService
    {
        Author GetAuthor(int id);
        IEnumerable<Author> GetAuthorList();
        ActionResultModel SaveAuthor(Author author);
        ActionResultModel DeleteAuthor(int id);
    }
}
