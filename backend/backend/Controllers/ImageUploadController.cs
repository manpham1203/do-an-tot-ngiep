using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _env;
        public ImageUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }
        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }
    }
}
