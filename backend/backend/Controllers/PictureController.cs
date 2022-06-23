using BLL.Picture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private PictureBLL pictureBLL;
        public PictureController()
        {
            pictureBLL = new PictureBLL();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await pictureBLL.Delete(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("published/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            var result = await pictureBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
        //[HttpGet("productId/{id}")]
        //public async Task<IActionResult> GetByProductId(string id)
        //{
        //    var result = await pictureBLL.GetByProductId(id);
        //    if (result == null)
        //    {
        //        return BadRequest();
        //    }
        //    for (int i = 0; i < result.Count; i++)
        //    {
        //        result[i].ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, result[i].Name);
        //    }
        //    return Ok(result);

        //}
    }
}
