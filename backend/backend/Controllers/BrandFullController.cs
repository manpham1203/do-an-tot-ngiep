using BLL.Brand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandFullController : ControllerBase
    {
        private readonly BrandFullBLL brandFullBLL;
        public BrandFullController()
        {
            brandFullBLL = new BrandFullBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brandFullVMs = await brandFullBLL.GetAll();
            if (brandFullVMs == null)
            {
                return NotFound();
            }
            return Ok(brandFullVMs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var brandFullVM = await brandFullBLL.GetById(id);
            if (brandFullVM == null)
            {
                return NotFound();
            }
            return Ok(brandFullVM);
        }
    }
}
