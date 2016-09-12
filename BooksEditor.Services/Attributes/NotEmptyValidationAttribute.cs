using System.ComponentModel.DataAnnotations;

namespace BooksEditor.Services.Attributes
{
    public class NotEmptyValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = false;

            if (value != null)
            {
                int[] authors = (int[])value;

                if (authors.Length != 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
