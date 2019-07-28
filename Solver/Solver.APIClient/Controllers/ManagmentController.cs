using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using Solver.BusinessLayer.Services;

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
        public IActionResult UploadFile([FromForm] string Identification)
        {
            
            return Ok(managmentService.ProcessTest(Request.Form.Files[0], Identification));
        }
    }
}
