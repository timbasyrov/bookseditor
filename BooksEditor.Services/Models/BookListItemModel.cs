using System.Collections.Generic;

namespace BooksEditor.Services.Models
{
    public class BookListItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<AuthorModel> Authors { get; set; }
        public int PageCount { get; set; }
        public string PublishingHouse { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string ImageUrl { get; set; }
    }
}
