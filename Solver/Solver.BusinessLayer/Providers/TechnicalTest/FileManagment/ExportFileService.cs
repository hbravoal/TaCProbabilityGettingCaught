using Microsoft.Extensions.Configuration;
using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ExportFileService : IExportFileService
    {
        private readonly IConfiguration configuration;

        public ExportFileService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        /// <summary>
        /// Genera .txt y retorna la ruta completa.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Response<string> GenerateFile(List<ProcessInformationResponse> model)
        {
            Response<string> response = new Response<string>
            {
                IsSuccess = false
            };
            try
            {
                string docPath = string.Format("{0}{1}", Environment.CurrentDirectory, configuration["FileGenerationPath:basePath"]);
                string docName = string.Format(configuration["FileGenerationPath:NameFile"], DateTime.Now.ToString("yyyyMMddHH"));
                using (StreamWriter file = new StreamWriter(Path.Combine(docPath, docName), true))
                {
                    foreach (var item in model)
                    {
                        List<string> items = new List<string>();
                        string data = string.Format("Case # {0}: {1}", item.Case, item.Detail.Count);
                        items.Add(data);
                        string linea = string.Join("|", items.ToArray());
                        file.WriteLine(linea);
                        
                    }
                    file.Dispose();
                }
                response.IsSuccess = true;
                response.Result = Path.Combine(configuration["FileGenerationPath:basePath"], docName);
            }
            catch (Exception ex)
            {
                response.Message.Add(new MessageResult
                {
                    Message = "Ocurrió un error generando el archivo, por favor intentar mas tarde."
                });
            }

            return response;
        }
    }
}
