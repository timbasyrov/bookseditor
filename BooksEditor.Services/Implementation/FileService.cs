using System;
using System.Web;
using System.IO;
using BooksEditor.Services.Models;

namespace BooksEditor.Services
{
    public class FileService : IFileService
    {
        public UploadFileResult UploadFile(HttpPostedFile file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    // Creating quite unique filename for uploaded file
                    var fileName = string.Format("{0}{1}", DateTime.Now.Ticks, fileExtension);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/files"), fileName);

                    file.SaveAs(path);

                    return new UploadFileResult()
                    {
                        IsSuccess = true,
                        Url = "/files/" + fileName
                    };
                }
                else return new UploadFileResult()
                {
                    IsSuccess = false,
                    Message = "File is empty"
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
