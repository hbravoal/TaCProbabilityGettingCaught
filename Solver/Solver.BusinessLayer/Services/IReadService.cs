using Solver.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solver.BusinessLayer.Services
{
    public interface IReadService
    {
        Response<bool> Read(string FilePath);
    }
}
