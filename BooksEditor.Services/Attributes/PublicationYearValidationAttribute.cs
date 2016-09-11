using System;
using System.ComponentModel.DataAnnotations;

namespace BooksEditor.Services.Attributes
{
    public class PublicationYearValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int publicationYear = (int)value;
                // Sometimes publication of book planned in near future
                int maxPublicationYearAllowed = DateTime.Now.Year + 3;

                if (publicationYear < 1800 || publicationYear > maxPublicationYearAllowed)
                {
                    return new ValidationResult(string.Format("Publication year must be in range from 1800 to {0}", maxPublicationYearAllowed));
                }
            }

            return ValidationResult.Success;
        }
    }
}
