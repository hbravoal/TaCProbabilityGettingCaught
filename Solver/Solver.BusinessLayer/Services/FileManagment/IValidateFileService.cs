using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.BusinessLayer.Services
{
    public interface IValidateFileService
    {
        Response<bool> Validate(string filePath);
    }
}
