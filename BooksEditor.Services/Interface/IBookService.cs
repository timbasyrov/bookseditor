using System.Collections.Generic;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public interface IBookService
    {
        Book GetBook(int id);
        IEnumerable<Book> GetBookList(BookListRequest request);
        ActionResultModel SaveBook(Book book);
        ActionResultModel DeleteBook(int id);
    }
}
