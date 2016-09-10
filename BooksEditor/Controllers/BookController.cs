using System.Collections.Generic;
using System.Web.Http;
using BooksEditor.Data.Models;
using BooksEditor.Services;
using BooksEditor.Services.Models;

namespace BooksEditor.Controllers
{
    public class BookController : ApiController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IEnumerable<Book> GetBookList([FromUri]BookListRequest request)
        {
            return _bookService.GetBookList(request);
        }

        [HttpGet]
        public Book GetBook(int id)
        {
            return _bookService.GetBook(id);
        }

        [HttpPost]
        public ActionResultModel SaveBook([FromBody]Book book)
        {
            return _bookService.SaveBook(book);
        }

        [HttpDelete]
        public ActionResultModel DeleteBook(int id)
        {
            return _bookService.DeleteBook(id);
        }
    }
}
