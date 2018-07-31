using System.Linq;
using System.Collections.Generic;
using BooksEditor.Data.Models;

namespace BooksEditor.Data
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksEditorContext _context;

        public AuthorRepository()
        {
            _context = BooksEditorContext.GetInstance();
        }

        public IEnumerable<Author> Authors => _context.Authors;

        public void Delete(int id)
        {
            var author = _context.Authors.FirstOrDefault(a => a.Id == id);

            if (author == null) return;

            foreach (var item in _context.Books.Where(b => b.Authors.Contains(author)))
            {
                item.Authors.Remove(author);
            }
            _context.Authors.Remove(author);
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Author author)
        {
            var authorEntry = _context.Authors.FirstOrDefault(a => a.Id == author.Id);

            if (authorEntry == null) return;

            authorEntry.Name = author.Name;
            authorEntry.Surname = author.Surname;
        }

        public void Add(Author author)
        {
            // Get max id
            author.Id = _context.Authors.Count == 0 ? 1 : _context.Authors.Max(a => a.Id) + 1;

            _context.Authors.Add(author);
        }
    }
}
