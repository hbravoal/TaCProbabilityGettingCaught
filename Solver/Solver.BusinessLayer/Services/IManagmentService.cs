using Solver.Common.Models;
namespace Solver.BusinessLayer.Services
{
    public interface IManagmentService
    {
        Response<string> ProcessTest(Microsoft.AspNetCore.Http.IFormFile file, string identification);
    }
}
