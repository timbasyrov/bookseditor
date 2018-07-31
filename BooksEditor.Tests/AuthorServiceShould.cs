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
    public class AuthorServiceShould
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private AuthorService _service;
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

            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Test book", Authors = new List<Author>() { _authors[0] }, PageCount = 100, PublicationYear = 1967 }
            };

            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockAuthorRepository.Setup(m => m.Authors).Returns(_authors);

            _mockBookRepository = new Mock<IBookRepository>();
            _service = new AuthorService(_mockAuthorRepository.Object, _mockBookRepository.Object);
        }

        [Test]
        public void DeleteAuthor_WhenCallsDeteleAuthorWithExistingId()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.FirstOrDefault(b => b.Id == id));

            // Act
            var result = _service.DeleteAuthor(2);

            // Assert
            _mockAuthorRepository.Verify(m => m.Delete(2));
            Assert.AreEqual(ActionResultState.Ok, result.State);
        }

        [Test]
        public void NotDeleteAuthor_WhenAuthorIsSoleAuthorOfTheExistingBook()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.FirstOrDefault(b => b.Id == id));
            _mockBookRepository.Setup(m => m.Books).Returns(_books);

            // Act
            var result = _service.DeleteAuthor(1);

            // Assert
            Assert.AreEqual(ActionResultState.Error, result.State);
        }

        [Test]
        public void DeleteAuthor_WhenCallsDeleteNonExistingAuthor()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.FirstOrDefault(b => b.Id == id));

            // Act
            var result = _service.DeleteAuthor(9);

            // Assert
            Assert.AreEqual(ActionResultState.NotFound, result.State);
        }

        [Test]
        public void ReturnAuthor_WhenCallsGetExistingAuthor()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.FirstOrDefault(b => b.Id == id));

            // Act
            var result = _service.GetAuthor(2);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
