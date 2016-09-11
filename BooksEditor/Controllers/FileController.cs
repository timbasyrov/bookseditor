using System.Web.Http;
using System.Web;
using BooksEditor.Services;
using BooksEditor.Services.Models;

namespace BooksEditor.Controllers
{
    public class FileController : ApiController
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public UploadFileResult Upload()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
                    HttpContext.Current.Request.Files[0] : null;

            return _fileService.UploadFile(file);
        }
    }
}
