using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.BusinessLayer.Services
{
    public interface IUploadService
    {
         Response<string> Load(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
