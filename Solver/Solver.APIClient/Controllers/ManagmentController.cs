using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Solver.BusinessLayer.Services;

namespace Solver.APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagmentController : ControllerBase
    {
        private readonly IUploadServices uploadServices;

        public ManagmentController(IUploadServices uploadServices, IHostingEnvironment hostingEnvironment)
        {
            this.uploadServices = uploadServices;
            _hostingEnvironment = hostingEnvironment;
        }
        private IHostingEnvironment _hostingEnvironment;



        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var ps = Request.Form.Files[0];
            uploadServices.Load(Request.Form.Files[0]);
            return new JsonResult("");
        }
    }
}
