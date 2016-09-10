using System.Collections.Generic;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public interface IBookService
    {
        BookModel GetBook(int id);
        IEnumerable<Book> GetBookList(BookListRequest request);
        ActionResultModel SaveBook(BookModel book);
        ActionResultModel DeleteBook(int id);
    }
}
