using System.Web;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public interface IFileService
    {
        UploadFileResult UploadFile(HttpPostedFile file);
    }
}
