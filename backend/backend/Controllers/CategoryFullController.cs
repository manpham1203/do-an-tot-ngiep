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
            var categoryFullVMs = await categoryFullBLL.GetAll();
            if (categoryFullVMs == null)
            {
                return NotFound();
            }
            return Ok(categoryFullVMs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var categoryFullVM = await categoryFullBLL.GetById(id);
            if (categoryFullVM == null)
            {
                return NotFound();
            }
            return Ok(categoryFullVM);
        }
    }
}
