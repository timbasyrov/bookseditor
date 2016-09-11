using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BooksEditor.Services;
using BooksEditor.Data;
using BooksEditor.Data.Models;
using BooksEditor.Services.Models;
using System.Linq;
using Moq;

namespace BooksEditor.Tests
{
    [TestClass]
    public class AuthorTests
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private AuthorService _service;
        private IList<Author> _authors;
        private IList<Book> _books;

        [TestInitialize]
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

        [TestMethod]
        public void Can_Save_Valid_Author()
        {
            // Arrange
            AuthorModel authorModel = new AuthorModel { Id = 4, Name = "Test Name", Surname = "Test Surname" };

            // Act
            var result = _service.SaveAuthor(authorModel);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Delete_Author()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.Where(b => b.Id == id).FirstOrDefault());

            // Act
            var result = _service.DeleteAuthor(2);

            // Assert
            _mockAuthorRepository.Verify(m => m.Delete(2));
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Not_Delete_Sole_Author()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.Where(b => b.Id == id).FirstOrDefault());
            _mockBookRepository.Setup(m => m.Books).Returns(_books);

            // Act
            var result = _service.DeleteAuthor(1);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Not_Delete_Missing_Author()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.Where(b => b.Id == id).FirstOrDefault());

            // Act
            var result = _service.DeleteAuthor(9);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Get_Author()
        {
            // Arrange
            _mockAuthorRepository.Setup(m => m.GetAuthor(It.IsAny<int>())).Returns((int id) => _authors.Where(b => b.Id == id).FirstOrDefault());

            // Act
            var result = _service.GetAuthor(2);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
