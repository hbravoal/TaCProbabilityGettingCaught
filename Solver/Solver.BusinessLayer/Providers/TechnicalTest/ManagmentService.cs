using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using Solver.Entities.Models;
using System.Collections.Generic;
using System.IO;
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
        private readonly IExportFileService exportFileService;
        #endregion

        #region Constructor
        public ManagmentService(IUploadService uploadServices, IReadFileService readService, IValidateFileService validateFileService,
            IProcessInformation processInformation, ITrackLogService    trackLogService, IExportFileService exportFileService)
        {
            this.uploadServices = uploadServices;
            this.readService = readService;
            this.validateFileService = validateFileService;
            this.processInformation = processInformation;
            this.trackLogService = trackLogService;
            this.exportFileService = exportFileService;
        }
        #endregion

        #region Implements
        public Response<string> ProcessTest(Microsoft.AspNetCore.Http.IFormFile file, string identification)
        {
            Response<string> responseProcessTest = new Response<string>();
            responseProcessTest.IsSuccess = false;
            Response<string> responseUpload = uploadServices.Load(file);

            TrackLog trackLog = new TrackLog{FileName = responseUpload.Result,Identification = identification};            
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
                            Response<string>  responseGenerateFile= exportFileService.GenerateFile(responseExecute.Result);

                            if (responseGenerateFile.IsSuccess)
                            {
                                responseProcessTest.Result = responseGenerateFile.Result;
                                responseProcessTest.IsSuccess = true;
                                trackLog.IsValid = true;
                            }
                            else
                            {
                                responseProcessTest.Message = new List<MessageResult> { new MessageResult { Message = "Ocurrió un error al Generar la información." } };
                                trackLog.TrackLogDetails.Add(new TrackLogDetail { Message = responseGenerateFile.Message.FirstOrDefault().ToString() });
                            }
                            
                        }
                        else
                        {
                            responseProcessTest.Message = new List<MessageResult> { new MessageResult { Message = "Ocurrió un error al Procesar la información." } };
                            trackLog.TrackLogDetails.Add(new TrackLogDetail{Message = responseExecute.Message.FirstOrDefault().ToString()});
                        }
                    }
                    else
                    {
                        responseProcessTest.Message = new List<MessageResult> { new MessageResult { Message = "Ocurrió un error al Leer la información." } };
                        trackLog.TrackLogDetails.Add(new TrackLogDetail{Message = response.Message.FirstOrDefault().ToString(),});
                    }
                }
                else
                {
                    responseProcessTest.Message = new List<MessageResult> { new MessageResult { Message = "Ocurrió un error al Validar la información." } };
                    trackLog.TrackLogDetails.Add(new TrackLogDetail{Message = resultValidate.Message.FirstOrDefault().Message.ToString(),});
                }
            }
            Response<TrackLog> responesTrack = this.trackLogService.Post(trackLog);
            return responseProcessTest;
        } 
        #endregion
    }
}
