using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Solver.BusinessLayer.Services;
using Solver.Common.Models;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Solver.APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagmentController : ControllerBase
    {

        private readonly IManagmentService managmentService;

        public ManagmentController(IManagmentService managmentService)
        {
            this.managmentService = managmentService;

        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadFile")]
        public HttpResponseMessage UploadFile([FromForm] string Identification)
        {
            Response<string> managMentServiceResponse= managmentService.ProcessTest(Request.Form.Files[0], Identification);
            if (managMentServiceResponse.IsSuccess)
            {
                var file = managMentServiceResponse.Result;

                // Response...
                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = file,
                    Inline = true  // false = prompt the user for downloading;  true = browser to try to show the file inline
                };

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileStream = new FileStream(file, FileMode.Open);
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = file
                };
                return response;

                
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                
            }
            return new HttpResponseMessage(HttpStatusCode.NotFound);


        }
    }
}
