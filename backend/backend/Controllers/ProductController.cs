
using BLL.Product;
using BO.ViewModels.Product;
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
    public class ProductController : ControllerBase
    {
        private readonly ProductBLL productBLL;
        private readonly ProductFullBLL productFullBLL;
        private IWebHostEnvironment iwebHostEnvironment;

        public ProductController(IWebHostEnvironment _iwebHostEnvironment)
        {
            productBLL = new ProductBLL();
            productFullBLL = new ProductFullBLL();
            this.iwebHostEnvironment = _iwebHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await productBLL.GetAll();
                return Ok(products);
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
                var product = await productBLL.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await productBLL.Create(model);
                    if (result && (model.Files != null || model.Files.Count != 0))
                    {
                        var saveFile = await SaveFile(model.Files, model.ImageNames);
                        if (!saveFile)
                        {
                            return BadRequest();
                        }
                    }
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] UpdateProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await productBLL.Update(id, model);
                    if (result && (model.Files != null || model.Files.Count != 0))
                    {
                        var saveFile = await SaveFile(model.Files, model.ImageNames);
                        if (!saveFile)
                        {
                            return BadRequest();
                        }
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await productBLL.Delete(id);
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
        [HttpGet("productfull")]
        public async Task<IActionResult> GetProductFullAll()
        {
            try
            {
                var products = await productFullBLL.GetAll();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("productfull/{id}")]
        public async Task<IActionResult> GetProductFullById(string id)
        {
            try
            {
                var product = await productFullBLL.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet("productfullgetbyslug/{slug}")]
        public async Task<IActionResult> ProductFullGetBySlug(string slug)
        {
            try
            {
                var productFullVM = await productFullBLL.GetBySlug(slug);

                if (productFullVM == null)
                {
                    return NotFound();
                }
                for (int i = 0; i < productFullVM.ProductImageVMs.Count; i++)
                {
                    productFullVM.ProductImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, productFullVM.ProductImageVMs[i].Name);
                }
                return Ok(productFullVM);
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

        [HttpPost("published/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            var result = await productBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        [HttpPost("deleted/{id}")]
        public async Task<IActionResult> Deleted(string id)
        {
            var result = await productBLL.Deleted(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpGet("GetAllProductDeleted")]
        public async Task<IActionResult> GetAllProductDeleted(bool deleted)
        {
            try
            {
                var brandVMs = await productBLL.GetAllProductDeleted(deleted);
                return Ok(brandVMs);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("AllProductName")]
        public async Task<IActionResult> AllProductName()
        {
            var resultFromBLL = await productBLL.AllProductName();
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }
        [HttpGet("AllProductNamedeleted")]
        public async Task<IActionResult> AllProductName(bool deleted)
        {
            var resultFromBLL = await productBLL.AllProductName(deleted);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }
        [HttpPost("AllProductNameadmin")]
        public async Task<IActionResult> AllProductNameAdmin(bool deleted, ProductFilterVM model)
        {
            var resultFromBLL = await productBLL.AllProductNameAdmin(deleted, model);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            return Ok(resultFromBLL);
        }
        [HttpGet("ProductRowAdmin/{id}")]
        public async Task<IActionResult> ProductRowAdmin(string id)
        {
            var resultFromBLL = await productBLL.ProductRowAdmin(id);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            resultFromBLL.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.ImageName);
            return Ok(resultFromBLL);
        }

        [HttpGet("productdetail/{slug}")]
        public async Task<IActionResult> ProductDetail(string slug)
        {
            var resultFromBLL = await productBLL.ProductDetail(slug);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            if (resultFromBLL.ProductImageVMs.Count > 0)
            {
                for (int i = 0; i < resultFromBLL.ProductImageVMs.Count; i++)
                {
                    resultFromBLL.ProductImageVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.ProductImageVMs[i].Name);
                }
            }
            return Ok(resultFromBLL);
        }

        [HttpPost("cartrows")]
        public async Task<IActionResult> CartRows(List<string> ids)
        {
            var resultFromBLL = await productBLL.CartRows(ids);
            if (resultFromBLL.Count ==0)
            {
                return BadRequest();
            }
            for (int i = 0; i < resultFromBLL.Count; i++)
            {
                resultFromBLL[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ImageName);
            }

            return Ok(resultFromBLL);
        }
    }
}
