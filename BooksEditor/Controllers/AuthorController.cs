using System.Collections.Generic;
using System.Web.Http;
using BooksEditor.Services;
using BooksEditor.Services.Models;
using System.Net.Http;
using System.Net;

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
        public HttpResponseMessage GetAuthor(int id)
        {
            var author = _authorService.GetAuthor(id);

            if (author == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new { });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, author);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddAuthor([FromBody]AuthorModel author)
        {
            var result = _authorService.AddAuthor(author);
            
            // For valid JSON response we use an empty object
            return Request.CreateResponse(HttpStatusCode.Created, new { });
        }

        [HttpPut]
        public HttpResponseMessage UpdateAuthor(int id, [FromBody]AuthorModel author)
        {           
            author.Id = id;
            var result = _authorService.UpdateAuthor(author);

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
        public HttpResponseMessage DeleteAuthor(int id)
        {
            var result = _authorService.DeleteAuthor(id);

            switch (result.State)
            {
                case ActionResultState.NotFound:
                    return Request.CreateResponse(HttpStatusCode.NotFound, result);
                case ActionResultState.Error:
                    return Request.CreateResponse((HttpStatusCode)422, result);
                default:
                    return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
        }
    }
}
