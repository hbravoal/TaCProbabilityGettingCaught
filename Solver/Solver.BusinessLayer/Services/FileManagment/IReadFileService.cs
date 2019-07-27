using Solver.Common.Models;
using Solver.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.BusinessLayer.Services
{
    public interface IReadFileService
    {
        Response<WorkingDays> Read(string FilePath);
        
    }
}
