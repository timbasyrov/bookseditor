using System.Linq;
using System.Collections.Generic;
using BooksEditor.Services;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;
using NUnit.Framework;
using Moq;

namespace BooksEditor.Tests
{
    public class BookServiceShould
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private BookService _service;
        private IList<Author> _authors;
        private IList<Book> _books;

        [SetUp]
        public void SetupContext()
        {
            _authors = new List<Author>
            {
                new Author { Id = 1, Name = "Ernest", Surname = "Hemingway" },
                new Author { Id = 2, Name = "Mark", Surname = "Twain" },
                new Author { Id = 3, Name = "Jules", Surname = "Verne" }
            };

            _books = new List<Book>();
            _books.Add(new Book { Id = 1, Title = "Title 1", Authors = new List<Author> { _authors[0], _authors[2] }, PageCount = 100, PublicationYear = 1999, PublishingHouse = "Microsoft Press", ISBN = "0-943396-04-2" });
            _books.Add(new Book { Id = 2, Title = "Title 2", Authors = new List<Author> { _authors[0] }, PageCount = 200, PublicationYear = 2007, PublishingHouse = "O'Reilly", ISBN = "978-0-557-50469-5" });
            _books.Add(new Book { Id = 3, Title = "Title 3", Authors = new List<Author> { _authors[1], _authors[2] }, PageCount = 300, PublicationYear = 2009, PublishingHouse = "Williams", ISBN = "978-1-56619-909-4" });
            _books.Add(new Book { Id = 4, Title = "Title 4", Authors = new List<Author> { _authors[2] }, PageCount = 296, PublicationYear = 2005, PublishingHouse = "Apress", ISBN = "1-4028-9462-7" });

            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockAuthorRepository.Setup(m => m.Authors).Returns(_authors);

            _mockBookRepository = new Mock<IBookRepository>();
            _service = new BookService(_mockBookRepository.Object, _mockAuthorRepository.Object);
        }

        [Test]
        public void ReturnNotFoundResultState_WhenTryingDeleteNonExistingBook()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.GetBook(It.IsAny<int>())).Returns((int id) => _books.FirstOrDefault(b => b.Id == id));
            var missingBookId = 9;

            // Act
            var result = _service.DeleteBook(missingBookId);

            // Assert
            _mockBookRepository.Verify(m => m.Delete(missingBookId), Times.Never);
            Assert.AreEqual(ActionResultState.NotFound, result.State);
        }

        [Test]
        public void ReturnBook_WhenCallsGetWithExistingId()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.GetBook(It.IsAny<int>())).Returns((int id) => _books.FirstOrDefault(b => b.Id == id));

            // Act
            var result = _service.GetBook(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void DeleteBook_WhenCallsDeleteWithExistingId()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.GetBook(It.IsAny<int>())).Returns((int id) => _books.FirstOrDefault(b => b.Id == id));

            // Act
            var result = _service.DeleteBook(1);

            // Assert
            _mockBookRepository.Verify(m => m.Delete(1));
            Assert.AreEqual(ActionResultState.Ok, result.State);
        }

        [Test]
        public void ReturnSortedBookList_WhenGetsBookListWithSortingParameter()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.Books).Returns(_books);

            // Act
            var result = _service.GetBookList(new BookListRequest { YearOrder = "desc" });

            // Assert
            Assert.AreEqual(3, result.First().Id);
        }
    }
}
