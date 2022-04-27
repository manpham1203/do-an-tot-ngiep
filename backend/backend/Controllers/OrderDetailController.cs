using BLL.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private OrderDetailBLL orderDetailBLL;
        public OrderDetailController()
        {
            orderDetailBLL = new OrderDetailBLL();
        }
        [HttpGet("GetListDetailByOrderId")]
        public async Task<IActionResult> GetListDetailByOrderIdUserId(string orderId, string userId)
        {
            try
            {
                var resultFromBLL = await orderDetailBLL.GetListDetailByOrderIdUserId(orderId, userId);
                if(resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.Count; i++)
                {
                    resultFromBLL[i].ProductOrderVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ProductOrderVM.ImageName);
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
