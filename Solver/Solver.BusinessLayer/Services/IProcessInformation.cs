using Solver.Common.Models;
using Solver.Entities.Models;
using System.Collections.Generic;

namespace Solver.BusinessLayer.Services
{
    public interface IProcessInformation
    {
        /// <summary>
        /// Encontrar la solución ídonea para no ser descubierto.
        /// </summary>
        /// <param name="model"></param>
        Response<List<ProcessInformationResponse>> Execute(WorkingDays model);
    }
}
