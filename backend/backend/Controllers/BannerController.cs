using BLL.Banner;
using BO.ViewModels.Banner;
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
    public class BannerController : ControllerBase
    {
        private BannerBLL bannerBLL;
        private IWebHostEnvironment iwebHostEnvironment;
        public BannerController(IWebHostEnvironment _iwebHostEnvironment)
        {
            bannerBLL = new BannerBLL();
            this.iwebHostEnvironment = _iwebHostEnvironment;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateBannerVM model)
        {
            try
            {
                var resultFromBLL = await bannerBLL.Create(model);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                if (resultFromBLL && model.File != null)
                {
                    var saveFile = await SaveFile(model.File, model.ImageName);
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] CreateBannerVM model)
        {
            try
            {
                var resultFromBLL = await bannerBLL.Update(id, model);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                if (resultFromBLL && model.File != null)
                {
                    var saveFile = await SaveFile(model.File, model.ImageName);
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var resultFromBLL = await bannerBLL.Delete(id);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("published")]
        public async Task<IActionResult> Published(string id)
        {
            try
            {
                var resultFromBLL = await bannerBLL.Published(id);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("deleted")]
        public async Task<IActionResult> Deleted(string id)
        {
            try
            {
                var resultFromBLL = await bannerBLL.Deleted(id);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(bool deleted, bool published)
        {
            try
            {
                var resultFromBLL = await bannerBLL.GetList(deleted, published);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.Count; i++)
                {
                    if (resultFromBLL[i].ImageName != null)
                    {
                        resultFromBLL[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ImageName);
                    }
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
        [NonAction]
        public async Task<bool> SaveFile(IFormFile file, string imgName)
        {
            var imagePath = Path.Combine(iwebHostEnvironment.ContentRootPath, "Photos", imgName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return true;
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> Pagination(bool deleted, int currentPage = 1, int limit = 10, string query = null)
        {
            try
            {
                var resultFromBLL = await bannerBLL.BannerPagination(currentPage, limit, query, deleted);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }

                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var resultFromBLL = await bannerBLL.GetById(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                resultFromBLL.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.ImageName);

                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
