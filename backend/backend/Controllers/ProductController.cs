using BLL.Product;
using BOL.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductBLL productBLL;
        public ProductController()
        {
            productBLL = new ProductBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productBLL.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await productBLL.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM model)
        {
            if (ModelState.IsValid)
            {
                var createProduct = await productBLL.Create(model);
                if (!createProduct)
                {
                    return BadRequest();
                }
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateProductVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await productBLL.Update(id, model);
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
            var result = await productBLL.Delete(id);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
