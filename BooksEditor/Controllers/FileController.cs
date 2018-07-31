using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BooksEditor.Services;
using BooksEditor.Services.Models;

namespace BooksEditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("Upload")]
        public async Task<UploadFileResult> Upload(IFormFile file)
        {
            return await _fileService.UploadFile(file);
        }
    }
}