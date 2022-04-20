using BLL.Order;
using BO.ViewModels.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderBLL orderBLL;
        public OrderController()
        {
            orderBLL = new OrderBLL();
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<OrderDetailVM> model)
        {
            try
            {
                var resulFromBLL = await orderBLL.Create(model);
                if (resulFromBLL)
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
    }
}
