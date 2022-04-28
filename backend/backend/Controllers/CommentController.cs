using BLL.Comment;
using BO.ViewModels.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ProductCmtBLL productCmtBLL;
        public CommentController()
        {
            productCmtBLL = new ProductCmtBLL();
        }
        [HttpPost("createProductCmt")]
        public async Task<IActionResult> CreateProductCmt(ProductCmtVM model)
        {
            try
            {
                var resultFromBLL = await productCmtBLL.Create(model);
                if (resultFromBLL == false)
                {
                    return BadRequest();
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("ProductCmtItem")]
        public async Task<IActionResult> ProductCmtItem(string id)
        {
            try
            {
                var resultFromDb = await productCmtBLL.CommentItem(id);
                if (resultFromDb == null)
                {
                    return BadRequest();
                }
                if (resultFromDb.ImageName != null)
                {
                    resultFromDb.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromDb.ImageName);
                }

                return Ok(resultFromDb);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("IdsOfProduct")]
        public async Task<IActionResult> IdsOfProduct(string id, int limit = 5, int currentPage = 1)
        {
            try
            {
                var resultFromDb = await productCmtBLL.IdsOfProduct(id, limit, currentPage);
                if (resultFromDb == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromDb);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
