﻿using System.Collections.Generic;
using BooksEditor.Data.Models;

namespace BooksEditor.Data
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> Authors { get; }
        Author GetAuthor(int id);
        void Delete(int id);
        void Add(Author author);
        void Update(Author author);
    }
}
