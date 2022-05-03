using BLL.Wishlist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private WishlistBLL wishlistBLL;
        public WishlistController()
        {
            wishlistBLL = new WishlistBLL();
        }
        [HttpGet]
        public async Task<IActionResult> CheckExists(string userId,string productId)
        {
            try
            {
                return Ok(await wishlistBLL.CheckExists(userId, productId));
            }
            catch
            {
                return Ok(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(string userId, string productId)
        {
            try
            {
                var resultFromBLL=await wishlistBLL.Create(userId,productId);
                if (resultFromBLL ==false)
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
        [HttpDelete]
        public async Task<IActionResult> Delete(string userId, string productId)
        {
            try
            {
                var resultFromBLL = await wishlistBLL.Delete(userId, productId);
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
    }
}
