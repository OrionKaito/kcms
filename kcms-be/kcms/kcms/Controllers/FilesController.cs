using KCMS.Ultitlies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace KCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FilesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        // GET: api/<FilesController>
        [HttpPost]
        public string UploadFile(IFormFile image)
        {
            return FileUlti.UploadFile(image, _hostingEnvironment); ;
        }
    }
}
