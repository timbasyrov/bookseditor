using Microsoft.AspNetCore.Http;
using BooksEditor.Services.Models;
using System.Threading.Tasks;

namespace BooksEditor.Services
{
    public interface IFileService
    {
        Task<UploadFileResult> UploadFile(IFormFile file);
    }
}
