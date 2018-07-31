using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using BooksEditor.Services.Models;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;

namespace BooksEditor.Services
{
    public class FileService : IFileService
    {
        // TODO: move FileMaxSize to configuration
        private const long FileMaxSize = 204800;
        private readonly IHostingEnvironment _hostingEnvironment;

        public FileService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<UploadFileResult> UploadFile(IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return new UploadFileResult()
                    {
                        IsSuccess = false,
                        Message = "File is empty"
                    };
                }

                if (file.Length > FileMaxSize)
                {
                    return new UploadFileResult()
                    {
                        IsSuccess = false,
                        Message = $"File size must be less then {FileMaxSize} bytes"
                    };
                }

                var fileExtension = Path.GetExtension(file.FileName);
                // Creating quite unique filename for uploaded file
                var fileName = string.Format("{0}{1}", DateTime.Now.Ticks, fileExtension);
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "files", fileName);
                    
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return new UploadFileResult()
                {
                    IsSuccess = true,
                    Url = "/files/" + fileName
                };
            }
            catch (Exception e)
            {
                return new UploadFileResult()
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }
    }
}
