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
            Authors.Add(new Author { Id = 1, Name = "Andrew", Surname = "Troelsen" });
            Authors.Add(new Author { Id = 2, Name = "Philip", Surname = "Japikse" });
            Authors.Add(new Author { Id = 3, Name = "Adam", Surname = "Freeman" });
            Authors.Add(new Author { Id = 4, Name = "Joel", Surname = "Spolsky" });
            Authors.Add(new Author { Id = 5, Name = "Frederick P.", Surname = "Brooks Jr." });
            Authors.Add(new Author { Id = 6, Name = "Steve", Surname = "McConnell" });
            Authors.Add(new Author { Id = 7, Name = "Joseph", Surname = "Albahari" });
            Authors.Add(new Author { Id = 8, Name = "Ben", Surname = "Albahari" });

            Books = new List<Book>();
            Books.Add(new Book { Id = 1, Title = "C# 6.0 and the .NET 4.6", Authors = new List<Author> { Authors[0], Authors[1] }, PageCount = 1603, ImageUrl = "/files/636092162924995115.jpg", PublicationYear = 2015, PublishingHouse = "Apress", ISBN = "978-1484213339" });
            Books.Add(new Book { Id = 2, Title = "Pro ASP.NET MVC 5", Authors = new List<Author> { Authors[2] }, PageCount = 785, ImageUrl = "/files/636092163025695240.jpg", PublicationYear = 2015, PublishingHouse = "Apress", ISBN = "978-1430265290" });
            Books.Add(new Book { Id = 3, Title = "Joel on Software", Authors = new List<Author> { Authors[3] }, PageCount = 347, ImageUrl = "/files/636092163113265601.jpg", PublicationYear = 2004, PublishingHouse = "Apress", ISBN = "978-1590593899" });
            Books.Add(new Book { Id = 4, Title = "The Mythical Man-Month", Authors = new List<Author> { Authors[4] }, PageCount = 336, ImageUrl = "/files/636092163279245936.jpg", PublicationYear = 1995, PublishingHouse = "Addison-Wesley", ISBN = "0-201-83595-9" });
            Books.Add(new Book { Id = 5, Title = "Code Complete", Authors = new List<Author> { Authors[5] }, PageCount = 960, ImageUrl = "/files/636092163335311120.jpg", PublicationYear = 2004, PublishingHouse = "Microsoft Press", ISBN = "0-7356-1967-0" });
            Books.Add(new Book { Id = 6, Title = "C# 6.0 in a Nutshell", Authors = new List<Author> { Authors[6], Authors[7] }, ImageUrl = "/files/636092163407717844.jpg", PageCount = 1138, PublicationYear = 2016, PublishingHouse = "O'Reilly", ISBN = "978-1-491-92706-9" });
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
