using BLL.BrandImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandImageController : ControllerBase
    {
        private BrandImageBLL brandImageBLL;
        public BrandImageController()
        {
            brandImageBLL = new BrandImageBLL();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await brandImageBLL.Delete(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("published/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            var result = await brandImageBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
    }
}
