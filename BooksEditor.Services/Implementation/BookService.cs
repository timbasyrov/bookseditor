using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book GetBook(int id)
        {
            return _bookRepository.GetBook(id);
        }

        public IEnumerable<Book> GetBookList(BookListRequest request)
        {
            var books = _bookRepository.Books;


            switch (request.TitleOrder)
            {
                case "asc" :
                case "desc":
                    books = books.OrderBy(string.Format("Title {0}", request.TitleOrder));
                    break;
            }

            switch (request.YearOrder)
            {
                case "asc":
                case "desc":
                    books = books.OrderBy(string.Format("PublicationYear {0}", request.YearOrder));
                    break;
            }

            return books;
        }

        public ActionResultModel SaveBook(Book book)
        {
            // Check if book have minimum one author
            _bookRepository.Save(book);
            return new ActionResultModel { IsSuccess = true };
        }

        public ActionResultModel DeleteBook(int id)
        {
            _bookRepository.Delete(id);
            return new ActionResultModel { IsSuccess = true };
        }
    }
}
