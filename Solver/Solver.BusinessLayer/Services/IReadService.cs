using Solver.Common.Models;
using Solver.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.BusinessLayer.Services
{
    public interface IReadService
    {
        Response<WorkingDays> Read(string FilePath);
    }
}
