using System.Collections.Generic;
using BooksEditor.Data.Models;

namespace BooksEditor.Data
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
        Book GetBook(int id);
        void Delete(int id);
        void Add(Book book);
        void Update(Book book);
    }
}
