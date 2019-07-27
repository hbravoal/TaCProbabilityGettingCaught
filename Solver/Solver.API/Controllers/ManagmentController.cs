using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solver.BusinessLayer.Services;

namespace Solver.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagmentController : ControllerBase
    {
        //private readonly IUploadServices uploadServices;

        //public ManagmentController(IUploadServices uploadServices)
        //{
        //    this.uploadServices = uploadServices;
        //}

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Managment/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Managment
        
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult Upload()
        {
            var solve= Request.Form.Files[0];
            //this.uploadServices.Load(Request.Form.Files[0]);
            return new JsonResult("s");
        }

        // PUT: api/Managment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
