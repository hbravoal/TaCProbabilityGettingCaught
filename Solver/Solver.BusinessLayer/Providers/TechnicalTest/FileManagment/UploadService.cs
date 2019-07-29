using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class UploadService : IUploadService
    {
   

        /// <summary>
        /// Sube el archivo al servidor y retorna la Ruta completa de éste.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>La ruta completa del archivo subido.</returns>
        public Response<string> Load(Microsoft.AspNetCore.Http.IFormFile file)
        {
            Response<string> response = new Response<string>
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
                    fullPath = Path.Combine(newPath, DateTime.Now.ToString("Hmmss")+fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        response.IsSuccess = true;
                        response.Message.Add(new MessageResult { Message = "Se ha guardado Archivo correctamente." });
                        response.Result = fullPath;
                    }
                }
            }
            catch (System.Exception ex)
            {
                
            }
            return response;
        }
    }
}
