using Solver.Common.Models;
using System.IO;

namespace Solver.BusinessLayer.Services
{
    public interface IManagmentService
    {
        Response<string> ProcessTest(Microsoft.AspNetCore.Http.IFormFile file, string identification);
    }
}
