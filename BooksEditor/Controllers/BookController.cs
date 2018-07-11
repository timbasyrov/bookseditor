using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BooksEditor.Services;
using BooksEditor.Services.Models;

namespace BooksEditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IEnumerable<BookListItemModel> GetBookList([FromQuery]BookListRequest request)
        {
            return _bookService.GetBookList(request);
        }

        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _bookService.GetBook(id);

            if (book == null)
            {
                return NotFound(new { });
            }
            else
            {
                return Ok(book);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody]BookModel book)
        {
            var result = _bookService.AddBook(book);
            if (result.State == ActionResultState.Error)
            {
                return BadRequest(new { result.Errors });
            }

            return Ok(new { });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody]BookModel book)
        {
            book.Id = id;
            var result = _bookService.UpdateBook(book);

            if (result.State == ActionResultState.NotFound)
            {
                return NotFound(new { result });
            }
            else
            {
                return Ok(new { });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = _bookService.DeleteBook(id);

            if (result.State == ActionResultState.NotFound)
            {
                return NotFound(result);
            }
            else
            {
                return Ok(new { });
            }
        }
    }
}