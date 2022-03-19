using BLL;
using BO.ViewModels.Brand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandBLL brandBLL;
        public BrandController()
        {
            brandBLL = new BrandBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brandVMs = await brandBLL.GetAll();
            if (brandVMs == null)
            {
                return NotFound();
            }
            return Ok(brandVMs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var brand = await brandBLL.GetById(id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBrandVM model)
        {
            var brandCreate = await brandBLL.Create(model);
            if (!brandCreate)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateBrandVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await brandBLL.Update(id, model);
                if (result == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await brandBLL.Delete(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
