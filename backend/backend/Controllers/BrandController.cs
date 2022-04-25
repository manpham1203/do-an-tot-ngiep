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
using BO.ViewModels.Product;
using BO.ViewModels.ProductImage;

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
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var brandVMs = await brandBLL.GetAll();
        //        return Ok(brandVMs);
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
        //        var brand = await brandBLL.GetById(id);
        //        if (brand == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(brand);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}

        //[HttpGet("getbyslug/{slug}")]
        //public async Task<IActionResult> GetBySlug(string slug)
        //{
        //    try
        //    {
        //        var brand = await brandFullBLL.GetBySlug(slug);
        //        if (brand == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(brand);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateBrandVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var brandCreate = await brandBLL.Create(model);
                    if (brandCreate && (model.File != null))
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
        public async Task<IActionResult> Update(string id, [FromForm] UpdateBrandVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await brandBLL.Update(id, model);
                    if (result && (model.File != null))
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

        //[HttpGet("brandfull")]
        //public async Task<IActionResult> BrandFullGetAll()
        //{
        //    try
        //    {
        //        var brandFullVM = await brandFullBLL.GetAll();

        //        if (brandFullVM == null)
        //        {
        //            return NotFound();
        //        }
        //        for (int i = 0; i < brandFullVM.Count; i++)
        //        {
        //            for (int j = 0; j < brandFullVM[i]BrandImageVMs.Count; j++)
        //            {
        //                brandFullVM[i].BrandImageVMs[j].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, brandFullVM[i].BrandImageVMs[j].Name);
        //            }
        //            //brandFullVM[i].ProductFullVMs = new List<ProductFullVM>();
        //            for (int p = 0; p < brandFullVM[i].ProductFullVMs.Count; p++)
        //            {
        //                //brandFullVM[i].ProductFullVMs[p].ProductImageVMs = new List<ProductImageVM>();
        //                for (int m = 0; m < brandFullVM[i].ProductFullVMs[p].ProductImageVMs.Count; m++)
        //                {
        //                    brandFullVM[i].ProductFullVMs[p].ProductImageVMs[m].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, brandFullVM[i].ProductFullVMs[p].ProductImageVMs[m].Name);
        //                }

        //            }
        //        }
        //        return Ok(brandFullVM);
        //    }

        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        //[HttpGet("brandfullgetbyid/{id}")]
        //public async Task<IActionResult> BrandFullGetById(string id)
        //{
        //    try
        //    {
        //        var brandFullVM = await brandFullBLL.GetById(id);

        //        if (brandFullVM == null)
        //        {
        //            return NotFound();
        //        }
        //        for (int i = 0; i < brandFullVM.BrandImageVMs.Count; i++)
        //        {
        //            brandFullVM.BrandImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, brandFullVM.BrandImageVMs[i].Name);
        //        }

        //        return Ok(brandFullVM);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

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
                if (brandFullVM.PictureVM != null)
                {
                    brandFullVM.PictureVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, brandFullVM.PictureVM.Name);
                }

                return Ok(brandFullVM);
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
            var result = await brandBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        [HttpPost("deleted/{id}")]
        public async Task<IActionResult> Deleted(string id)
        {
            var result = await brandBLL.Deleted(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpGet("GetAllBrandDeleted")]
        public async Task<IActionResult> GetAllBrandDeleted(bool deleted)
        {
            try
            {
                var brandVMs = await brandBLL.GetAllBrandDeleted(deleted);
                return Ok(brandVMs);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("AllBrandWithProductCard")]
        public async Task<IActionResult> AllBrandWithProductCard()
        {
            try
            {
                var resultFromBLL = await brandBLL.AllBrandWithProductCard();
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.Count == 0)
                {
                    return Ok(new List<BrandNameVM>());
                }
                if (resultFromBLL.Count > 0)
                {
                    for (int i = 0; i < resultFromBLL.Count; i++)
                    {
                        for (int j = 0; j < resultFromBLL[i].ProductCardVMs.Count; j++)
                        {
                            resultFromBLL[i].ProductCardVMs[j].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ProductCardVMs[j].ImageName);
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

        [HttpGet("BrandWithProductCard")]
        public async Task<IActionResult> BrandWithProductCard(string id)
        {
            try
            {
                var resultFromBLL = await brandBLL.BrandWithProductCard(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int j = 0; j < resultFromBLL.ProductCardVMs.Count; j++)
                {
                    resultFromBLL.ProductCardVMs[j].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.ProductCardVMs[j].ImageName);
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("allbrandname")]
        public async Task<IActionResult> AllBrandName()
        {
            var resultFromBLL = await brandBLL.AllBrandName();
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }
        [HttpGet("allbrandnamedeleted")]
        public async Task<IActionResult> AllBrandName(bool deleted)
        {
            var resultFromBLL = await brandBLL.AllBrandName(deleted);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }
        [HttpPost("allbrandnameadmindeleted")]
        public async Task<IActionResult> AllBrandNameAdmin(bool deleted, BrandFilterVM model)
        {
            var resultFromBLL = await brandBLL.AllBrandNameAdmin(deleted, model);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }

        [HttpGet("brandrowadmin/{id}")]
        public async Task<IActionResult> BrandRowAdmin(string id)
        {
            var resultFromBLL = await brandBLL.BrandRowAdmin(id);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }

    }
}
