namespace BooksEditor.Services.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int[] Authors { get; set; }
        public int PageCount { get; set; }
        public string PublishingHouse { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }
    }
}
