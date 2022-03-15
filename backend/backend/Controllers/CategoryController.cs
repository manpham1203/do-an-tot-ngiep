using BLL.Category;
using BOL.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryBLL categoryBLL;
        public CategoryController()
        {
            categoryBLL = new CategoryBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categoryVMs = await categoryBLL.GetAll();
            if (categoryVMs == null)
            {
                return NotFound();
            }
            return Ok(categoryVMs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var category=await categoryBLL.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var categoryCreate = await categoryBLL.Create(model);
                if (categoryCreate)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var categoryUpdate = await categoryBLL.Update(id, model);
                if (categoryUpdate == false)
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
            var categoryDelete = await categoryBLL.Delete(id);
            if (categoryDelete == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
