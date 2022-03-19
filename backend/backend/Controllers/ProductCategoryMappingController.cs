using BLL.ProductCategoryMapping;
using BO.ViewModels.ProductCategoryMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryMappingController : ControllerBase
    {
        private readonly ProductCategoryMappingBLL objBLL;
        public ProductCategoryMappingController()
        {
            objBLL = new ProductCategoryMappingBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await objBLL.GetAll();
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id, string type)
        {
            var objList = await objBLL.GetById(id, type);
            if (objList == null)
            {
                return NotFound();
            }
            return Ok(objList);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryMappingVM model)
        {
            var mappingCreate = await objBLL.Create(model);
            if (!mappingCreate)
            {
                return NotFound();
            }
            return StatusCode(StatusCodes.Status201Created);
        }
        
        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(string id, string type)
        {
            var result = await objBLL.Delete(id, type);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
