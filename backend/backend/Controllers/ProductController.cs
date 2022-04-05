
using BLL.Product;
using BO.ViewModels.Product;
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
            try
            {
                var products = await productBLL.GetAll();
                return Ok(products);
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
                var product = await productBLL.GetById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM model)
        {
            try
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

            catch
            {
                return BadRequest();
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateProductVM model)
        {
            try
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
            catch
            {
                return BadRequest();
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await productBLL.Delete(id);
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
        [HttpGet("productfull")]
        public async Task<IActionResult> GetProductFullAll()
        {
            try
            {
                var products = await productBLL.GetProductFullAll();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet("productfull/{id}")]
        public async Task<IActionResult> GetProductFullById(string id)
        {
            try
            {
                var product = await productBLL.GetProductFullById(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
