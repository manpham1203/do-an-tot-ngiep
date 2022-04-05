using BLL;
using BO.ViewModels.Brand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using BO.ViewModels;
using System;
using BLL.Brand;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandBLL brandBLL;
        private readonly BrandFullBLL brandFullBLL;
        //private IWebHostEnvironment _env;
        private IWebHostEnvironment iwebHostEnvironment;
        public BrandController(IWebHostEnvironment _iwebHostEnvironment)
        {
            brandBLL = new BrandBLL();
            brandFullBLL = new BrandFullBLL();
            this.iwebHostEnvironment = _iwebHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brandVMs = await brandBLL.GetAll();
                return Ok(brandVMs);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var brand = await brandBLL.GetById(id);
                if (brand == null)
                {
                    return NotFound();
                }
                return Ok(brand);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("getbyslug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            try
            {
                var brand = await brandFullBLL.GetBySlug(slug);
                if (brand == null)
                {
                    return NotFound();
                }
                return Ok(brand);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var brandCreate = await brandBLL.Create(model);
                    if (brandCreate && (model.formFile != null || model.formFile.Count != 0))
                    {
                        var saveFile = await SaveFile(model.formFile, model.imageName);
                        if (!saveFile)
                        {
                            return BadRequest();
                        }
                    }
                    return StatusCode(StatusCodes.Status201Created);
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBrandVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await brandBLL.Update(id, model);
                    if (result == false)
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
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await brandBLL.Delete(id);
                if (result == false)
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

        [HttpGet("brandfull")]
        public async Task<IActionResult> BrandFullGetAll()
        {
            try
            {
                var brandFullVMs = await brandFullBLL.GetAll();
                if (brandFullVMs == null)
                {
                    return NotFound();
                }
                return Ok(brandFullVMs);
            }

            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("brandfullgetbyid/{id}")]
        public async Task<IActionResult> BrandFullGetById(string id)
        {
            try
            {
                var brandFullVM = await brandFullBLL.GetById(id);

                if (brandFullVM == null)
                {
                    return NotFound();
                }
                for (int i = 0; i < brandFullVM.BrandImageVMs.Count; i++)
                {
                    brandFullVM.BrandImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, brandFullVM.BrandImageVMs[i].Name);
                }
                return Ok(brandFullVM);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("brandfullgetbyslug/{slug}")]
        public async Task<IActionResult> BrandFullGetBySlug(string slug)
        {
            try
            {
                var brandFullVM = await brandFullBLL.GetBySlug(slug);

                if (brandFullVM == null)
                {
                    return NotFound();
                }
                for (int i = 0; i < brandFullVM.BrandImageVMs.Count; i++)
                {
                    brandFullVM.BrandImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, brandFullVM.BrandImageVMs[i].Name);
                }
                return Ok(brandFullVM);
            }
            catch
            {
                return BadRequest();
            }
        }

        [NonAction]
        public async Task<bool> SaveFile(List<IFormFile> files, List<string> imgName)
        {

            for (int i = 0; i < files.Count; i++)
            {
                //imageName = imgName[i];
                //imageName = imageName + Path.GetExtension(files[i].FileName);

                var imagePath = Path.Combine(iwebHostEnvironment.ContentRootPath, "Photos", imgName[i]);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await files[i].CopyToAsync(fileStream);
                }

            }
            return true;
        }



        //[HttpPost("SaveFile")]
        //public IActionResult SaveFile([FromForm] List<IFormFile> file)
        //{
        //    var httpRequest = Request.Form;
        //    var postedFile = httpRequest.Files[0];
        //    string filename=postedFile.FileName;
        //    var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
        //    using(var stream = new FileStream(physicalPath, FileMode.Create))
        //    {
        //        postedFile.CopyTo(stream);
        //    }
        //    return Ok(filename);
        //}
        //[HttpPost("SaveFile")]
        //public async Task<IActionResult> SaveFile([FromForm] List<IFormFile> files)
        //{
        //    try
        //    {
        //        var result = new List<FileUploadResult>();
        //        foreach (var file in files)
        //        {
        //            var path = Path.Combine(this.iwebHostEnvironment.WebRootPath, "images", file.FileName);
        //            var stream = new FileStream(path, FileMode.Create);
        //            await file.CopyToAsync(stream);
        //            result.Add(new FileUploadResult() { Name = file.FileName, Length = file.Length });
        //        }
        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        //[NonAction]
        //public async Task<IActionResult> SaveFile([FromForm] List<IFormFile> files)
        //{
        //    try
        //    {
        //        var result = new List<FileUploadResult>();
        //        foreach (var file in files)
        //        {
        //            var path = Path.Combine(this.iwebHostEnvironment.WebRootPath, "images", file.FileName);
        //            var stream = new FileStream(path, FileMode.Create);
        //            await file.CopyToAsync(stream);
        //            result.Add(new FileUploadResult() { Name = file.FileName, Length = file.Length });
        //        }
        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
