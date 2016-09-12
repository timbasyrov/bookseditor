using System.Collections.Generic;
using System.Web.Http;
using BooksEditor.Services;
using BooksEditor.Services.Models;
using System.Net.Http;
using System.Net;

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
        public IEnumerable<BookListItemModel> GetBookList([FromUri]BookListRequest request)
        {
            return _bookService.GetBookList(request);
        }

        [HttpGet]
        public HttpResponseMessage GetBook(int id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, book);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddBook([FromBody]BookModel book)
        {
            var result = _bookService.AddBook(book);

            return Request.CreateResponse(HttpStatusCode.Created, new { });
        }

        [HttpPut]
        public HttpResponseMessage UpdateBook(int id, [FromBody]BookModel book)
        {
            book.Id = id;
            var result = _bookService.UpdateBook(book);

            if (result.State == ActionResultState.NotFound)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteBook(int id)
        {
            var result = _bookService.DeleteBook(id);

            if (result.State == ActionResultState.NotFound)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
        }
    }
}
