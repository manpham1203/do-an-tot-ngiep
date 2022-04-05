using BLL.ProductCategory;
using BO.ViewModels.ProductCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryMappingController : ControllerBase
    {
        private readonly ProductCategoryBLL objBLL;
        public ProductCategoryMappingController()
        {
            objBLL = new ProductCategoryBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await objBLL.GetAll();
                return Ok(list);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string id, string type)
        {
            try
            {
                var objList = await objBLL.GetById(id, type);
                return Ok(objList);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryVM model)
        {
            try
            {
                var mappingCreate = await objBLL.Create(model);
                if (!mappingCreate)
                {
                    return BadRequest();
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpDelete("DeleteById")]
        public async Task<IActionResult> Delete(string id, string type)
        {
            try
            {
                var result = await objBLL.Delete(id, type);
                if (result == false)
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
