using BLL.CategoryImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryImageController : ControllerBase
    {
        private CategoryImageBLL categoryImageBLL;
        public CategoryImageController()
        {
            categoryImageBLL = new CategoryImageBLL();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await categoryImageBLL.Delete(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost("published/{id}")]
        public async Task<IActionResult> Published(string id)
        {
            var result = await categoryImageBLL.Published(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();

        }
    }
}
