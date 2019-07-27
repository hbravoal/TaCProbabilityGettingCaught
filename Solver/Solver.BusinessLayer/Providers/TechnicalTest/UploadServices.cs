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
        private readonly IReadService readService;

        public UploadServices(IReadService readService)
        {
            this.readService = readService;
        }
        public Response<bool> Load(Microsoft.AspNetCore.Http.IFormFile file)
        {
            Response<bool> response = new Response<bool>
            {
                IsSuccess = false
            };
            string fullPath = string.Empty;
            try
            {
                

                string folderName = "Uploads";
                string webRootPath = "Files";                
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = System.Net.Http.Headers.ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    fullPath = Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        response.IsSuccess = true;
                        response.Message.Add(new MessageResult { Message = "Se ha guardado Archivo correctamente." });
                    }
                }
                readService.Read(fullPath);
            }
            catch (System.Exception ex)
            {
                
            }
            return response;
        }
    }
}
