using BLL.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryFullController : ControllerBase
    {
        private readonly CategoryFullBLL categoryFullBLL;
        public CategoryFullController()
        {
            categoryFullBLL = new CategoryFullBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categoryFullVMs = await categoryFullBLL.GetAll();
                return Ok(categoryFullVMs);
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
                var categoryFullVM = await categoryFullBLL.GetById(id);
                if(categoryFullVM == null)
                {
                    return NotFound();
                }
                return Ok(categoryFullVM);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
