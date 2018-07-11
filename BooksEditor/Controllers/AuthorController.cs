using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BooksEditor.Services;
using BooksEditor.Services.Models;

namespace BooksEditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IEnumerable<AuthorModel> GetAuthorList()
        {
            return _authorService.GetAuthorList();
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthor(int id)
        {
            var author = _authorService.GetAuthor(id);

            if (author == null)
            {
                return NotFound(new { });
            }
            else
            {
                return Ok(author);
            }
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody]AuthorModel author)
        {
            var result = _authorService.AddAuthor(author);

            if (result.State == ActionResultState.Error)
            {
                return BadRequest(new { result.Errors });
            }

            // For valid JSON response we use an empty object
            return Ok(new { });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody]AuthorModel author)
        {
            author.Id = id;
            var result = _authorService.UpdateAuthor(author);

            if (result.State == ActionResultState.NotFound)
            {
                return NotFound(new { result.Errors });
            }
            else
            {
                return Ok(new { });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var result = _authorService.DeleteAuthor(id);

            switch (result.State)
            {
                case ActionResultState.NotFound:
                    return NotFound(result);
                case ActionResultState.Error:
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, result);
                default:
                    return Ok(new { });
            }
        }
    }
}