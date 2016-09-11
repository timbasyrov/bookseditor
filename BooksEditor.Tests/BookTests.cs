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
    public class BookTests
    {
        private Mock<IAuthorRepository> _mockAuthorRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private BookService _service;
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

        [TestMethod]
        public void Can_Save_Valid_Book()
        {
            // Arrange
            BookModel bookModel = new BookModel { Id = 5, Title = "Title 5", Authors = new int[1] { 1 }, PageCount = 300, PublicationYear = 2004, PublishingHouse = "Apress", ISBN = "1-4028-9462-7" };

            // Act
            var result = _service.SaveBook(bookModel);

            // Assert
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Not_Delete_Missing_Book()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.GetBook(It.IsAny<int>())).Returns((int id) => _books.Where(b => b.Id == id).FirstOrDefault());

            // Act
            var result = _service.DeleteBook(9);

            // Assert
            _mockBookRepository.Verify(m => m.Delete(9), Times.Never);
            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Get_Book()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.GetBook(It.IsAny<int>())).Returns((int id) => _books.Where(b => b.Id == id).FirstOrDefault());

            // Act
            var result = _service.GetBook(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Can_Delete_Book()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.GetBook(It.IsAny<int>())).Returns((int id) => _books.Where(b => b.Id == id).FirstOrDefault());

            // Act
            var result = _service.DeleteBook(1);

            // Assert
            _mockBookRepository.Verify(m => m.Delete(1));
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void Can_Get_Sorted_Book_List()
        {
            // Arrange
            _mockBookRepository.Setup(m => m.Books).Returns(_books);

            // Act
            var result = _service.GetBookList(new BookListRequest { YearOrder = "desc" });

            // Assert
            Assert.AreEqual(3, result.First().Id);
        }

        [TestMethod]
        public void Can_Not_Save_Book_Without_Authors()
        {
            // Arrange
            BookModel bookModel = new BookModel { Id = 5, Title = "Title 5", Authors = new int[] { }, PageCount = 300, PublicationYear = 2004, PublishingHouse = "Apress", ISBN = "1-4028-9462-7" };

            // Act
            var result = _service.SaveBook(bookModel);

            // Assert
            Assert.IsFalse(result.IsSuccess);
        }
    }
}
