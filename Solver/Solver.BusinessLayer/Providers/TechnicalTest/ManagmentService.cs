using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;
using System.Collections.Generic;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ManagmentService : IManagmentService
    {
        #region Properties
        private readonly IUploadService uploadServices;
        private readonly IReadFileService readService;
        private readonly IValidateFileService validateFileService;
        private readonly IProcessInformation processInformation;
        #endregion

        #region Constructor
        public ManagmentService(IUploadService uploadServices, IReadFileService readService, IValidateFileService validateFileService,
            IProcessInformation processInformation)
        {
            this.uploadServices = uploadServices;
            this.readService = readService;
            this.validateFileService = validateFileService;
            this.processInformation = processInformation;
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
                    if (response.IsSuccess)
                    {
                        Response<List<ProcessInformationResponse>>  responseExecute= processInformation.Execute(response.Result);
                    }
                }
            }
            return new Response<bool>();
        } 
        #endregion
    }
}
