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
        private readonly IBookRepository _bookRepository;

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
        }

        public Author GetAuthor(int id)
        {
            return _authorRepository.GetAuthor(id);
        }

        public IEnumerable<Author> GetAuthorList()
        {
            return _authorRepository.Authors;
        }

        public ActionResultModel SaveAuthor(Author author)
        {
            _authorRepository.Save(author);
            return new ActionResultModel { IsSuccess = true };
        }

        public ActionResultModel DeleteAuthor(int id)
        {
            ActionResultModel result = new ActionResultModel();
            // Author has books where he is sole author
            if (_bookRepository.Books.Any(b => b.Authors.Any(a => a.Id == id) && b.Authors.Count == 1))
            {
                result.IsSuccess = false;
                result.Errors.Add("This author has a books, where he is the sole author, so it can not be removed");
            }
            else
            {
                _authorRepository.Delete(id);
                result.IsSuccess = true;
            }

            return result;
        }
    }
}
