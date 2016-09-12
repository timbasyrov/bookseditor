using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;
using AutoMapper;

namespace BooksEditor.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;

            var autoMapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookModel>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .AfterMap((src, dest) =>
                    {
                        dest.Authors = src.Authors.Select(a => a.Id).ToArray();
                    });

                cfg.CreateMap<BookModel, Book>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .AfterMap((src, dest) =>
                    {
                        dest.Authors = _authorRepository.Authors.Where(a => src.Authors.Contains(a.Id)).ToList();
                    });

                cfg.CreateMap<Author, AuthorModel>();

                cfg.CreateMap<Book, BookListItemModel>()
                    .ForMember(dest => dest.Authors, opt => opt.Ignore())
                    .AfterMap((src, dest) =>
                    {
                        dest.Authors = _mapper.Map<IEnumerable<AuthorModel>>(src.Authors).ToList();
                    });
            });

            _mapper = autoMapConfig.CreateMapper();
        }

        public BookModel GetBook(int id)
        {
            var bookEntity = _bookRepository.GetBook(id);

            return _mapper.Map<BookModel>(bookEntity);
        }

        public IEnumerable<BookListItemModel> GetBookList(BookListRequest request)
        {
            var books = _bookRepository.Books;

            switch (request.TitleOrder)
            {
                case "asc" :
                case "desc":
                    books = books.OrderBy(string.Format("Title {0}", request.TitleOrder));
                    break;
            }

            switch (request.YearOrder)
            {
                case "asc":
                case "desc":
                    books = books.OrderBy(string.Format("PublicationYear {0}", request.YearOrder));
                    break;
            }

            return _mapper.Map<IEnumerable<BookListItemModel>>(books);
        }

        public ActionResultModel AddBook(BookModel bookModel)
        {
            var bookEntity = _mapper.Map<Book>(bookModel);

            _bookRepository.Add(bookEntity);
            return new ActionResultModel { State = ActionResultState.Ok };
        }

        public ActionResultModel UpdateBook(BookModel bookModel)
        {
            var bookEntity = _mapper.Map<Book>(bookModel);

            ActionResultModel result = new ActionResultModel();

            if (_bookRepository.GetBook(bookEntity.Id) == null)
            {
                result.Errors.Add("Author not found");
                result.State = ActionResultState.NotFound;
            }
            else
            {
                _bookRepository.Update(bookEntity);
                result.State = ActionResultState.Ok;
            }

            return result;
        }

        public ActionResultModel DeleteBook(int id)
        {
            ActionResultModel result = new ActionResultModel();

            if (_bookRepository.GetBook(id) != null)
            {
                _bookRepository.Delete(id);
                result.State = ActionResultState.Ok;
            }
            else
            {
                result.State = ActionResultState.NotFound;
            }

            return result;
        }
    }
}
