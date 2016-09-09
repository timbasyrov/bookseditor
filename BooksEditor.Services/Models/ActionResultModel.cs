using System.Collections.Generic;

namespace BooksEditor.Services.Models
{
    public class ActionResultModel
    {
        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }

        public ActionResultModel()
        {
            Errors = new List<string>();
        }
    }
}