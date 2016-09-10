using System.ComponentModel.DataAnnotations;

namespace BooksEditor.Services.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Author's name must be less 20 symbols")]
        public string Name { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Author's surname must be less 20 symbols")]
        public string Surname { get; set; }
    }
}
