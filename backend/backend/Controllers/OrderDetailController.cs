using BLL.OrderDetail;
using BO.ViewModels.OrderDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        [HttpGet("GetListDetailByOrderIdUserId")]
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
    
        [HttpGet("GetListDetailByOrderId")]
        public async Task<IActionResult> GetListDetailByOrderId(string orderId)
        {
            try
            {
                var resultFromBLL = await orderDetailBLL.GetListDetailByOrderId(orderId);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.Count == 0)
                {
                    return Ok(new List<OrderDetailVM>());
                }
                for(int i = 0; i < resultFromBLL.Count; i++)
                {
                    if (resultFromBLL[i].ProductOrderVM.ImageName != null)
                    {
                        resultFromBLL[i].ProductOrderVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].ProductOrderVM.ImageName);
                    }
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
