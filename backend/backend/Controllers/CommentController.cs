using BLL.Comment;
using BO.ViewModels.Comment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return Ok(resultFromDb);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("IdsOfProduct")]
        public async Task<IActionResult> IdsOfProduct(string id)
        {
            try
            {
                var resultFromDb = await productCmtBLL.IdsOfProduct(id);
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
