using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BooksEditor.Services;
using BooksEditor.Data;
using BooksEditor.Data.Models;
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
                new Book { Id = 1, Title = "Test book", Authors = new List<Author>() { new Author { Id = 1, Name = "Test Name", Surname = "Test Surname" } }, PageCount = 100, PublicationYear = 2016 }
            };

            _mockAuthorRepository = new Mock<IAuthorRepository>();
            _mockAuthorRepository.Setup(m => m.Authors).Returns(_authors);

            _mockBookRepository = new Mock<IBookRepository>();
            _mockBookRepository.Setup(m => m.Books).Returns(_books);
            _service = new AuthorService(_mockAuthorRepository.Object, _mockBookRepository.Object);
        }

        [TestMethod]
        public void Can_Save_Valid_Author()
        {
            // Arrange
            Author author = new Author { Id = 4, Name = "Test Name", Surname = "Test Surname" };

            // Act
            var result = _service.SaveAuthor(author);

            // Assert
            _mockAuthorRepository.Verify(m => m.Save(author));
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Delete_Author()
        {
            // Act
            var result = _service.DeleteAuthor(2);

            // Assert
            _mockAuthorRepository.Verify(m => m.Delete(2));
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Not_Delete_Sole_Author()
        {
            // Act
            var result = _service.DeleteAuthor(1);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
