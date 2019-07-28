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
        public Response<string> GenerateFile(List<ProcessInformationResponse> model)
        {
            string docPath = string.Format("{0}{1}",Environment.CurrentDirectory,configuration["FileGenerationPath:basePath"]);
            string docName = string.Format(configuration["FileGenerationPath:NameFile"],DateTime.Now.ToString("yyyyMMddHH"));
            using (StreamWriter file = new StreamWriter(Path.Combine(docPath, docName), true))
            {
                foreach (var item in model)
                {
                    List<string> items = new List<string>();
                    string data = string.Format("Case # {0}: {1}",item.Case,item.Detail.Count);
                    items.Add(data);
                    string linea = string.Join("|", items.ToArray());
                    file.WriteLine(linea);
                }
            }
            return new Response<string>();
        }
    }
}
