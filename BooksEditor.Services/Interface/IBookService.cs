using System.Collections.Generic;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public interface IBookService
    {
        BookModel GetBook(int id);
        IEnumerable<BookListItemModel> GetBookList(BookListRequest request);
        ActionResultModel SaveBook(BookModel book);
        ActionResultModel DeleteBook(int id);
    }
}
