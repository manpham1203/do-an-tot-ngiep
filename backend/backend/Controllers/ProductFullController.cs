using BLL.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFullController : ControllerBase
    {
        private readonly ProductFullBLL productFullBLL;
        public ProductFullController()
        {
            productFullBLL = new ProductFullBLL();
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productFullBLL.GetAll();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(string id)
        {
            var product = await productFullBLL.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
