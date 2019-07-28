using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace Solver.BusinessLayer.Providers.TechnicalTest
{
    public class ManagmentService : IManagmentService
    {
        #region Properties
        private readonly IUploadService uploadServices;
        private readonly IReadFileService readService;
        private readonly IValidateFileService validateFileService;
        private readonly IProcessInformation processInformation;
        private readonly ITrackLogService trackLogService;
        #endregion

        #region Constructor
        public ManagmentService(IUploadService uploadServices, IReadFileService readService, IValidateFileService validateFileService,
            IProcessInformation processInformation, ITrackLogService    trackLogService)
        {
            this.uploadServices = uploadServices;
            this.readService = readService;
            this.validateFileService = validateFileService;
            this.processInformation = processInformation;
            this.trackLogService = trackLogService;
        }
        #endregion

        #region Implements
        public Response<bool> ProcessTest(Microsoft.AspNetCore.Http.IFormFile file, string identification)
        {
            
            Response<string> responseUpload = uploadServices.Load(file);

            TrackLog trackLog = new TrackLog
            {
                FileName = responseUpload.Result,
                Identification = identification
            };
            Response<TrackLog> responesTrack=this.trackLogService.Post(trackLog);
            trackLog.TrackLogDetails = new List<TrackLogDetail>();


            if (responseUpload.IsSuccess)
            {
                Response<bool> resultValidate = this.validateFileService.Validate(responseUpload.Result);
                if (resultValidate.IsSuccess)
                {
                    Response<WorkingDays> response = this.readService.Read(responseUpload.Result);
                    if (response.IsSuccess)
                    {
                        Response<List<ProcessInformationResponse>>responseExecute= processInformation.Execute(response.Result);
                        if (responseExecute.IsSuccess)
                        {

                        }
                        else
                        {
                            trackLog.TrackLogDetails.Add(new TrackLogDetail
                            {
                                Message = responseUpload.Message.FirstOrDefault().ToString(),
                            });
                        }
                    }
                    else
                    {
                        trackLog.TrackLogDetails.Add(new TrackLogDetail
                        {
                            Message = responseUpload.Message.FirstOrDefault().ToString(),
                        });
                    }
                }
                else
                {
                    trackLog.TrackLogDetails.Add(new TrackLogDetail
                    {
                        Message = responseUpload.Message.FirstOrDefault().ToString(),
                    });
                }
            }
            this.trackLogService.Put(trackLog);
            return new Response<bool>();
        } 
        #endregion
    }
}
