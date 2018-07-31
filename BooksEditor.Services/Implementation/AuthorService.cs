using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        // TODO: move MaxRecords option to configuration
        private const int MaxRecords = 50;

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
            return _mapper.Map<IEnumerable<AuthorModel>>(_authorRepository.Authors);
        }

        public ActionResultModel AddAuthor(AuthorModel authorModel)
        {
            if (_authorRepository.Authors.Count() >= MaxRecords)
            {
                var result = new ActionResultModel();
                result.State = ActionResultState.Error;
                result.Errors.Add($"Limit of {MaxRecords} records is reached");
                return result;
            }
            var authorEntity = _mapper.Map<Author>(authorModel);

            _authorRepository.Add(authorEntity);
            return new ActionResultModel { State = ActionResultState.Ok };
        }

        public ActionResultModel UpdateAuthor(AuthorModel authorModel)
        {
            var authorEntity = _mapper.Map<Author>(authorModel);

            var result = new ActionResultModel { State = ActionResultState.Ok };

            if (_authorRepository.GetAuthor(authorEntity.Id) == null)
            {
                result.State = ActionResultState.NotFound;
                result.Errors.Add("Author not found");
            }

            _authorRepository.Update(authorEntity);
            return result;
        }

        public ActionResultModel DeleteAuthor(int id)
        {
            var result = new ActionResultModel();

            // Author has books where he is sole author
            if (_bookRepository.Books.Any(b => b.Authors.Any(a => a.Id == id) && b.Authors.Count == 1))
            {
                result.State = ActionResultState.Error;
                result.Errors.Add("This author has a book(s), where he is the sole author, so it can not be removed");
            }
            else
            {
                if (_authorRepository.GetAuthor(id) != null)
                {
                    _authorRepository.Delete(id);
                    result.State = ActionResultState.Ok;
                }
                else
                {
                    result.State = ActionResultState.NotFound;
                    result.Errors.Add("Author not found");
                }
            }

            return result;
        }
    }
}
