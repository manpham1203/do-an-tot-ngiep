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
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var categoryVMs = await categoryBLL.GetAll();
        //        return Ok(categoryVMs);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    try
        //    {
        //        var category = await categoryBLL.GetById(id);
        //        if (category == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(category);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryCreate = await categoryBLL.Create(model);
                    if (categoryCreate && (model.File != null))
                    {
                        var saveFile = await SaveFile(model.File, model.ImageName);
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
                    if (categoryUpdate && (model.File != null))
                    {
                        var saveFile = await SaveFile(model.File, model.ImageName);
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
                if (categoryFullVM.PictureVM != null)
                {
                    categoryFullVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, categoryFullVM.PictureVM.Name);
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
                if (categoryFullVM.PictureVM != null)
                {
                    categoryFullVM.PictureVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, categoryFullVM.PictureVM.Name);
                }

                return Ok(categoryFullVM);
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

        [HttpPost("published/{id}")]
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

        [HttpGet("AllCategoryWithProductCard")]
        public async Task<IActionResult> AllCategoryWithProductCard()
       {
            try
            {
                var resultFromBLL = await categoryBLL.AllCategoryWithProductCard();
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.Count == 0)
                {
                    return Ok(new List<CategoryNameVM>());
                }
                if (resultFromBLL!=null)
                {
                    for (int i = 0; i < resultFromBLL.Count; i++)
                    {
                        if (resultFromBLL[i].ProductCardVMs != null)
                        {
                            for (int j = 0; j < resultFromBLL[i].ProductCardVMs.Count; j++)
                            {
                                resultFromBLL[i].ProductCardVMs[j].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ProductCardVMs[j].ImageName);
                            }
                        }
                    }
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("allcategoryname")]
        public async Task<IActionResult> AllCategoryName()
        {
            var resultFromBLL = await categoryBLL.AllCategoryName();
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }

        [HttpGet("allcategorynamedeleted")]
        public async Task<IActionResult> AllCategoryName(bool deleted)
        {
            var resultFromBLL = await categoryBLL.AllCategoryName(deleted);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }

        [HttpPost("allcategorynameadmindeleted")]
        public async Task<IActionResult> AllCategoryNameAdmin(bool deleted, CategoryFilterVM model)
        {
            var resultFromBLL = await categoryBLL.AllCategoryNameAdmin(deleted, model);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }


        [HttpGet("categoryrowadmin/{id}")]
        public async Task<IActionResult> CategoryRowAdmin(string id)
        {
            var resultFromBLL = await categoryBLL.CategoryRowAdmin(id);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }
        [HttpGet("CategoryDetail")]
        public async Task<IActionResult> CategoryDetail(string id)
        {
            try
            {
                var resultFromBLL = await categoryBLL.CategoryDetail(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.Image != null)
                {
                    resultFromBLL.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.Image);

                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
    
        [HttpGet("categorychart")]
        public async Task<IActionResult> CategoryChart()
        {
            try
            {
                var resultFromBLL = await categoryBLL.CategoryChart();
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
    }
}
