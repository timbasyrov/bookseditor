using System.Collections.Generic;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;
using System.Linq;

namespace BooksEditor.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Author GetAuthor(int id)
        {
            return _authorRepository.GetAuthor(id);
        }

        public IEnumerable<Author> GetAuthorList(AuthorListRequest request)
        {
            var authors = _authorRepository.Authors;

            switch (request.NameOrder)
            {
                case "asc" :
                    authors = authors.OrderBy(a => a.Name);
                    break;
                case "desc":
                    authors = authors.OrderByDescending(a => a.Name);
                    break;
            }

            switch (request.SurnameOrder)
            {
                case "asc":
                    authors = authors.OrderBy(a => a.Surname);
                    break;
                case "desc":
                    authors = authors.OrderByDescending(a => a.Surname);
                    break;
            }

            return authors;
        }

        public ActionResultModel SaveAuthor(Author author)
        {
            _authorRepository.Save(author);
            return new ActionResultModel { IsSuccess = true };
        }

        public ActionResultModel DeleteAuthor(int id)
        {
            // TODO: Check if author have books where he is only one author
            _authorRepository.Delete(id);
            return new ActionResultModel { IsSuccess = true };
        }
    }
}
