using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.BusinessLayer.Services
{
    public interface IUploadServices
    {
         Response<bool> Load(Microsoft.AspNetCore.Http.IFormFile file);
    }
}
