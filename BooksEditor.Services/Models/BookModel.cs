﻿using System.ComponentModel.DataAnnotations;
using BooksEditor.Services.Attributes;

namespace BooksEditor.Services.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Title must be less 30 symbols")]
        public string Title { get; set; }

        [NotEmptyValidation(ErrorMessage = "Book must have at least one author") ]
        public int[] Authors { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "Page count must be in range from 1 to 10000")]
        public int PageCount { get; set; }

        [MaxLength(30, ErrorMessage = "Name of the publishing house must be less 30 symbols")]
        public string PublishingHouse { get; set; }

        [PublicationYearValidation]
        public int PublicationYear { get; set; }

        [ISBNValidation(ErrorMessage = "Invalid ISBN")]
        public string ISBN { get; set; }

        public string ImageUrl { get; set; }
    }
}
