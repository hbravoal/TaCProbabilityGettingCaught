using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Solver.BusinessLayer.Services;
using Solver.Common.Models;

namespace Solver.APIClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagmentController : ControllerBase
    {
        private readonly IUploadServices uploadServices;

        public ManagmentController(IUploadServices uploadServices)
        {
            this.uploadServices = uploadServices;
            
        }
        

        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadFile")]
        public IActionResult UploadFile([FromForm] string Identification)
        {
            



            return Ok(uploadServices.Load(Request.Form.Files[0]));
        }
    }
}
