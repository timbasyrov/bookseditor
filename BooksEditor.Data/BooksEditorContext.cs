using System;
using System.Collections.Generic;
using BooksEditor.Data.Models;

namespace BooksEditor.Data
{
    public class BooksEditorContext
    {
        private static BooksEditorContext _instance;
        public IList<Author> Authors { get; set; }
        public IList<Book> Books { get; set; }

        private BooksEditorContext()
        {
            Authors = new List<Author>();
            Author author1 = new Author { Id = 1, Name = "Name 1", Surname = "Surname 1" };
            Author author2 = new Author { Id = 2, Name = "Name 2", Surname = "Surname 2" };
            Author author3 = new Author { Id = 3, Name = "Name 3", Surname = "Surname 3" };
            Authors.Add(author1);
            Authors.Add(author2);
            Authors.Add(author3);

            Books = new List<Book>();
            Books.Add(new Book { Id = 1, Title = "Title 1", Authors = new List<Author> { author1, author2 }, PageCount = 100, PublicationYear = 1999, PublishingHouse = "Microsoft", ISBN = "0-987-654-32-1-0" });
            Books.Add(new Book { Id = 2, Title = "Title 2", Authors = new List<Author> { author1 }, PageCount = 200, PublicationYear = 2005, PublishingHouse = "O'Reilly", ISBN = "0-789-456-23-0-1" });
            Books.Add(new Book { Id = 3, Title = "Title 3", Authors = new List<Author> { author2, author3 }, PageCount = 300, PublicationYear = 2009, PublishingHouse = "Williams", ISBN = "9-785-542-21-1-9" });
        }

        public static BooksEditorContext GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BooksEditorContext();
            }
            return _instance;
        }
    }
}
