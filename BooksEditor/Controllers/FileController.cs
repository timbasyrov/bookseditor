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
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("Upload")]
        public async Task<UploadFileResult> Upload(IFormFile file)
        {
            //var file = HttpContext.Request.Current.Request.Files.Count > 0 ?
            //        HttpContext.Current.Request.Files[0] : null;            

            return await _fileService.UploadFile(file);
        }
    }
}