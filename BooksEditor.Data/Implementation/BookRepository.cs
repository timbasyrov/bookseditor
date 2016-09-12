using System.Linq;
using System.Collections.Generic;
using BooksEditor.Data.Models;

namespace BooksEditor.Data
{
    public class BookRepository : IBookRepository
    {
        private BooksEditorContext _context;

        public BookRepository()
        {
            _context = BooksEditorContext.GetInstance();
        }

        public IEnumerable<Book> Books
        {
            get
            {
                return _context.Books;
            }
        }

        public void Delete(int id)
        {
            Book book = _context.Books.FirstOrDefault(a => a.Id == id);

            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(a => a.Id == id);
        }

        public void Add(Book book)
        {
                // Get max Id value
                book.Id = _context.Books.Count == 0 ? 1 : _context.Books.Max(a => a.Id) + 1;

                _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            Book bookEntry = _context.Books.FirstOrDefault(a => a.Id == book.Id);

            if (bookEntry != null)
            {
                bookEntry.Title = book.Title;
                bookEntry.Authors = book.Authors;
                bookEntry.PageCount = book.PageCount;
                bookEntry.PublicationYear = book.PublicationYear;
                bookEntry.PublishingHouse = book.PublishingHouse;
                bookEntry.ISBN = book.ISBN;
                bookEntry.ImageUrl = book.ImageUrl;
            }
        }
    }
}
