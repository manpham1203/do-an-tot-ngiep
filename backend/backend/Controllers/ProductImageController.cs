using BLL.ProductImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private ProductImageBLL productImageBLL;
        public ProductImageController()
        {
            productImageBLL = new ProductImageBLL();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await productImageBLL.Delete(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("published/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            var result = await productImageBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        [HttpGet("productId/{id}")]
        public async Task<IActionResult> GetByProductId(string id)
        {
            var result = await productImageBLL.GetByProductId(id);
            if (result==null)
            {
                return BadRequest();
            }
            for (int i = 0; i < result.Count; i++)
            {
                result[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, result[i].Name);
            }
            return Ok(result);

        }
    }
}
