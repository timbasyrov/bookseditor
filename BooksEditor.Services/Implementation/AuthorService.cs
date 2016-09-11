using System.Collections.Generic;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;
using System.Linq;
using AutoMapper;

namespace BooksEditor.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;

            var autoMapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorModel>();
                cfg.CreateMap<AuthorModel, Author>();
            });

            _mapper = autoMapConfig.CreateMapper();
        }

        public AuthorModel GetAuthor(int id)
        {
            return _mapper.Map<AuthorModel>(_authorRepository.GetAuthor(id));
        }

        public IEnumerable<AuthorModel> GetAuthorList()
        {
            return  _mapper.Map<IEnumerable<AuthorModel>>(_authorRepository.Authors);
        }

        public ActionResultModel SaveAuthor(AuthorModel authorModel)
        {
            var authorEntity = _mapper.Map<Author>(authorModel);

            _authorRepository.Save(authorEntity);
            return new ActionResultModel { IsSuccess = true };
        }

        public ActionResultModel DeleteAuthor(int id)
        {
            ActionResultModel result = new ActionResultModel() { IsSuccess = false };
            // Author has books where he is sole author
            if (_bookRepository.Books.Any(b => b.Authors.Any(a => a.Id == id) && b.Authors.Count == 1))
            {
                result.IsSuccess = false;
                result.Errors.Add("This author has a book(s), where he is the sole author, so it can not be removed");
            }
            else
            {
                if (_authorRepository.GetAuthor(id) != null)
                {
                    _authorRepository.Delete(id);
                    result.IsSuccess = true;
                }
                else
                {
                    result.Errors.Add("Author entity not found");
                }
            }

            return result;
        }
    }
}
