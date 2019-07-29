using Solver.Common.Models;
using System.Collections.Generic;

namespace Solver.BusinessLayer.Services
{
    public interface IExportFileService
    {
        Response<string> GenerateFile(List<ProcessInformationResponse> model);
    }
}
