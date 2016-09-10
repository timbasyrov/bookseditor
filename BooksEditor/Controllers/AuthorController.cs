using System.Collections.Generic;
using System.Web.Http;
using BooksEditor.Services;
using BooksEditor.Services.Models;

namespace BooksEditor.Controllers
{
    public class AuthorController : ApiController
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

        [HttpGet]
        public AuthorModel GetAuthor(int id)
        {
            return _authorService.GetAuthor(id);
        }

        [HttpPost]
        public ActionResultModel SaveAuthor([FromBody]AuthorModel author)
        {
            return _authorService.SaveAuthor(author);
        }

        [HttpDelete]
        public ActionResultModel DeleteAuthor(int id)
        {
            return _authorService.DeleteAuthor(id);
        }
    }
}
