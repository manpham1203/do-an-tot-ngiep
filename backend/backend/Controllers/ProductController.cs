
using BLL.Product;
using BO.ViewModels.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    if (result && (model.Files != null))
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
                    if (result == false)
                    {
                        return BadRequest();
                    }
                    if (result && (model.Files != null))
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
        //[HttpGet("productfull")]
        //public async Task<IActionResult> GetProductFullAll()
        //{
        //    try
        //    {
        //        var products = await productFullBLL.GetAll();
        //        return Ok(products);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}
        //[HttpGet("productfull/{id}")]
        //public async Task<IActionResult> GetProductFullById(string id)
        //{
        //    try
        //    {
        //        var product = await productFullBLL.GetById(id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(product);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }

        //}

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
                if (productFullVM.PictureVMs != null)
                {
                    for (int i = 0; i < productFullVM.PictureVMs.Count; i++)
                    {
                        productFullVM.PictureVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, productFullVM.PictureVMs[i].Name);
                    }
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

        [HttpGet("pricerange")]
        public async Task<IActionResult> PriceRange()
        {
            var resultFromBLL = await productBLL.PriceRange();
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
            if (resultFromBLL.PictureVMs.Count > 0)
            {
                for (int i = 0; i < resultFromBLL.PictureVMs.Count; i++)
                {
                    resultFromBLL.PictureVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.PictureVMs[i].Name);
                }
            }
            return Ok(resultFromBLL);
        }
        [HttpGet("productdetailAdmin/{slug}")]
        public async Task<IActionResult> ProductDetailAdmin(string slug)
        {
            var resultFromBLL = await productBLL.ProductDetailAdmin(slug);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            if (resultFromBLL.PictureVMs.Count > 0)
            {
                for (int i = 0; i < resultFromBLL.PictureVMs.Count; i++)
                {
                    resultFromBLL.PictureVMs[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.PictureVMs[i].Name);
                }
            }
            return Ok(resultFromBLL);
        }

        [HttpPost("cartrows")]
        public async Task<IActionResult> CartRows(List<string> ids)
        {
            var resultFromBLL = await productBLL.CartRows(ids);
            if (resultFromBLL.Count == 0)
            {
                return BadRequest();
            }
            for (int i = 0; i < resultFromBLL.Count; i++)
            {
                resultFromBLL[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ImageName);
            }

            return Ok(resultFromBLL);
        }

        [HttpGet("newproductwidget")]
        public async Task<IActionResult> NewProductWidget()
        {
            var resultFromBLL = await productBLL.NewProductWidget();
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            if (resultFromBLL.Count == 0)
            {
                return Ok(new List<ProductWidgetVM>());
            }
            for (int i = 0; i < resultFromBLL.Count; i++)
            {
                resultFromBLL[i].ImgSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ImgName);

            }
            return Ok(resultFromBLL);
        }

        [HttpPost("productfilter")]
        public async Task<IActionResult> ProductFilter(ProductFilterVM model)
        {
            var resultFromBLL = await productBLL.ProductFilter(model);
            if (resultFromBLL == null)
            {
                return BadRequest();
            }
            if (resultFromBLL.Products.Count == 0)
            {
                return Ok(new ProductPaginationVM());
            }
            for (int i = 0; i < resultFromBLL.Products.Count; i++)
            {
                if (resultFromBLL.Products[i].ImageName != null)
                {
                    resultFromBLL.Products[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.Products[i].ImageName);
                }
            }
            return Ok(resultFromBLL);
        }

        [HttpPut("increaseview")]
        public async Task<IActionResult> IncreaseView(string id)
        {
            try
            {
                var resultFromDb = await productBLL.IncreaseView(id);
                if (resultFromDb == false)
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

        [HttpGet("ProductWishlist")]
        public async Task<IActionResult> ProductWishlist(string userId)
        {
            try
            {
                var resultFromBLL = await productBLL.ProductWishlist(userId);
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

        [HttpPut("PublishedTrueList")]
        public async Task<IActionResult> PublishedTrueList(List<string> ids)
        {
            try
            {
                var resultFromBLL = await productBLL.PublishedTrueList(ids);
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
        [HttpPut("PublishedFalseList")]
        public async Task<IActionResult> PublishedFalseList(List<string> ids)
        {
            try
            {
                var resultFromBLL = await productBLL.PublishedFalseList(ids);
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
        [HttpPut("DeletedFalseList")]
        public async Task<IActionResult> DeletedFalseList(List<string> ids)
        {
            try
            {
                var resultFromBLL = await productBLL.DeletedFalseList(ids);
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
        [HttpPut("DeletedTrueList")]
        public async Task<IActionResult> DeletedTrueList(List<string> ids)
        {
            try
            {
                var resultFromBLL = await productBLL.DeletedTrueList(ids);
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
        [HttpGet("MostBought")]
        public async Task<IActionResult> MostBought(int take)
        {
            try
            {
                var resultFromBLL = await productBLL.MostBought(take);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.Count > 0)
                {
                    for (int i = 0; i < resultFromBLL.Count; i++)
                    {
                        if (resultFromBLL[i].ImageName != null)
                        {
                            resultFromBLL[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ImageName);
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
        [HttpGet("NewProduct")]
        public async Task<IActionResult> NewProduct(int take)
        {
            try
            {
                var resultFromBLL = await productBLL.ListProductCard();
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                resultFromBLL = resultFromBLL.Take(take).ToList();
                for(int i = 0; i < resultFromBLL.Count; i++)
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
        [HttpGet("OnSale")]
        public async Task<IActionResult> OnSale(int take)
        {
            try
            {
                var resultFromBLL = await productBLL.OnSale(take);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.Count == 0)
                {
                    return Ok(new List<ProductCardVM>());
                }
                for(int i = 0; i < resultFromBLL.Count(); i++)
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
    
        

    }
}
