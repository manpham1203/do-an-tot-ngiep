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
                for (int i = 0; i < resultFromDb.Children.Count; i++)
                {
                    if (resultFromDb.ImageName != null)
                    {
                        resultFromDb.Children[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromDb.Children[i].ImageName);

                    }
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

        [HttpPost("repcmtproduct")]
        public async Task<IActionResult> RepCmtProduct(string parentId, string content, string userId)
        {
            try
            {
                var resultFromBLL = await productCmtBLL.RepCmt(parentId, content, userId);
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
        [HttpGet("cmtChildrenProduct")]
        public async Task<IActionResult> CmtChildrenproduct(string parentId)
        {
            try
            {
                var resultFromBLL = await productCmtBLL.CmtChildren(parentId);
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
    }
}
