using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ManagmentService : IManagmentService
    {
        #region Properties
        private readonly IUploadService uploadServices;
        private readonly IReadFileService readService;
        private readonly IValidateFileService validateFileService;
        #endregion

        #region Constructor
        public ManagmentService(IUploadService uploadServices, IReadFileService readService, IValidateFileService validateFileService)
        {
            this.uploadServices = uploadServices;
            this.readService = readService;
            this.validateFileService = validateFileService;
        }
        #endregion

        #region Implements
        public Response<bool> ProcessTest(Microsoft.AspNetCore.Http.IFormFile file, string identification)
        {
            
            Response<string> responseUpload = uploadServices.Load(file);

            if (responseUpload.IsSuccess)
            {
                Response<bool> resultValidate = this.validateFileService.Validate(responseUpload.Result);
                if (resultValidate.IsSuccess)
                {
                    Response<WorkingDays> response = this.readService.Read(responseUpload.Result);
                }
            }
            return new Response<bool>();
        } 
        #endregion
    }
}
