using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;
using System.Collections.Generic;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ManagmentService : IManagmentService
    {
        private readonly IUploadServices uploadServices;
        private readonly IReadService readService;

        public ManagmentService(IUploadServices uploadServices, IReadService readService)
        {
            this.uploadServices = uploadServices;
            this.readService = readService;
        }
        public Response<bool> ProcessTest(Microsoft.AspNetCore.Http.IFormFile file, string identification)
        {

            Response<string> responseUpload= uploadServices.Load(file);

            if (responseUpload.IsSuccess)
            {
                Response<WorkingDays> responseRead= readService.Read(responseUpload.Result);

            }
            return new Response<bool>();
        }
    }
}
