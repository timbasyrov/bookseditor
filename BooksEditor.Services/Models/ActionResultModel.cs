using System.Collections.Generic;

namespace BooksEditor.Services.Models
{
    public class ActionResultModel
    {
        public ActionResultState State { get; set; }

        public List<string> Errors { get; set; }

        public ActionResultModel()
        {
            Errors = new List<string>();
        }
    }

    public enum ActionResultState : byte
    {
        Ok = 0,
        NotFound = 1,

        Error = 10
    }
}