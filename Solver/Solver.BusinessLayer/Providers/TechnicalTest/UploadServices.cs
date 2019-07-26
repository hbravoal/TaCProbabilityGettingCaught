using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class UploadServices : IUploadServices
    {
        public Response<bool> Load(Microsoft.AspNetCore.Http.IFormFile file )
        {
            try
            {
                
                string folderName = "Upload";
                string webRootPath = "Path";
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return new Response<bool>();
            }
            catch (System.Exception ex)
            {
                return new Response<bool>();
            }
        }
    }
}
