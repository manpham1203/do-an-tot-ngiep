using BLL.Category;
using BO.ViewModels.Category;
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
            try
            {
                var categoryVMs = await categoryBLL.GetAll();
                return Ok(categoryVMs);
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
                var category = await categoryBLL.GetById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryCreate = await categoryBLL.Create(model);
                    if (categoryCreate)
                    {
                        return StatusCode(StatusCodes.Status201Created);
                    }
                    return BadRequest();
                }
                catch
                {
                    return BadRequest();
                }

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
                try
                {
                    var categoryUpdate = await categoryBLL.Update(id, model);
                    if (categoryUpdate == false)
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
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var categoryDelete = await categoryBLL.Delete(id);
                if (categoryDelete == false)
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
