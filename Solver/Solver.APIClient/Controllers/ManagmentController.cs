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
        public ActionResult UploadFile([FromForm] string Identification)
        {
            Response<string> response = new Response<string>();
            Response<string> managMentServiceResponse= managmentService.ProcessTest(Request.Form.Files[0], Identification);
            if (managMentServiceResponse.IsSuccess)
            {

                response.Result = managMentServiceResponse.Result;
                response.IsSuccess = true;


            }
            return Ok(response);
        


        }
    }
}
