using BLL.Category;
using BO.ViewModels.Category;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryBLL categoryBLL;
        private CategoryFullBLL categoryFullBLL;
        private IWebHostEnvironment iwebHostEnvironment;
        public CategoryController(IWebHostEnvironment _iwebHostEnvironment)
        {
            categoryBLL = new CategoryBLL();
            categoryFullBLL = new CategoryFullBLL();
            this.iwebHostEnvironment = _iwebHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categoryVMs = await categoryBLL.GetAll();
                return Ok(categoryVMs);
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
                var category = await categoryBLL.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryCreate = await categoryBLL.Create(model);
                    if (categoryCreate && (model.Files != null || model.Files.Count != 0))
                    {
                        var saveFile = await SaveFile(model.Files, model.ImageNames);
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
        public async Task<IActionResult> Update(string id, [FromForm] UpdateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryUpdate = await categoryBLL.Update(id, model);
                    if (categoryUpdate && (model.Files != null || model.Files.Count != 0))
                    {
                        var saveFile = await SaveFile(model.Files, model.ImageNames);
                        if (!saveFile)
                        {
                            return BadRequest();
                        }
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
                var categoryDelete = await categoryBLL.Delete(id);
                if (categoryDelete == false)
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
        [HttpGet("categoryfull")]
        public async Task<IActionResult> CategoryFullGetAll()
        {
            try
            {
                var brandFullVMs = await categoryFullBLL.GetAll();
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

        [HttpGet("categoryfullgetbyid/{id}")]
        public async Task<IActionResult> CategoryFullGetById(string id)
        {
            try
            {
                var categoryFullVM = await categoryFullBLL.GetById(id);

                if (categoryFullVM == null)
                {
                    return NotFound();
                }
                for (int i = 0; i < categoryFullVM.CategoryImageVMs.Count; i++)
                {
                    categoryFullVM.CategoryImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, categoryFullVM.CategoryImageVMs[i].Name);
                }
                return Ok(categoryFullVM);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("categoryfullgetbyslug/{slug}")]
        public async Task<IActionResult> CategoryFullGetBySlug(string slug)
        {
            try
            {
                var categoryFullVM = await categoryFullBLL.GetBySlug(slug);

                if (categoryFullVM == null)
                {
                    return NotFound();
                }
                for (int i = 0; i < categoryFullVM.CategoryImageVMs.Count; i++)
                {
                    categoryFullVM.CategoryImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, categoryFullVM.CategoryImageVMs[i].Name);
                }
                return Ok(categoryFullVM);
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

        [HttpPost("pulished/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            var result = await categoryBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        [HttpPost("deleted/{id}")]
        public async Task<IActionResult> Deleted(string id)
        {
            var result = await categoryBLL.Deleted(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpGet("GetAllCategoryDeleted")]
        public async Task<IActionResult> GetAllCategoryDeleted(bool deleted)
        {
            try
            {
                var categoryVMs = await categoryBLL.GetAllCategoryDeleted(deleted);
                return Ok(categoryVMs);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
