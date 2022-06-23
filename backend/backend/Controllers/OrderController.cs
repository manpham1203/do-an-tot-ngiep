using BLL.Order;
using BO.ViewModels.Order;
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
    public class OrderController : ControllerBase
    {
        private OrderBLL orderBLL;
        public OrderController()
        {
            orderBLL = new OrderBLL();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CheckoutVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resulFromBLL = await orderBLL.Create(model);
                    if (resulFromBLL)
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
            catch
            {
                return BadRequest();

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByUserId(string userId)
        {
            try
            {
                var resultFromBLL = await orderBLL.GetOrderByUserId(userId);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.Count; i++)
                {
                    for (int j = 0; j < resultFromBLL[i].OrderDetailVMs.Count; j++)
                    {
                        resultFromBLL[i].OrderDetailVMs[j].ProductOrderVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].OrderDetailVMs[j].ProductOrderVM.ImageName);
                    }
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetOrderByUserIdState0")]
        public async Task<IActionResult> GetOrderByUserIdState0(string userId)
        {
            try
            {
                var resultFromBLL = await orderBLL.GetOrderByUserIdState0(userId);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.Count; i++)
                {
                    for (int j = 0; j < resultFromBLL[i].OrderDetailVMs.Count; j++)
                    {
                        resultFromBLL[i].OrderDetailVMs[j].ProductOrderVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].OrderDetailVMs[j].ProductOrderVM.ImageName);
                    }
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetOrderByUserIdState4")]
        public async Task<IActionResult> GetOrderByUserIdState4(string userId)
        {
            try
            {
                var resultFromBLL = await orderBLL.GetOrderByUserIdState4(userId);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("PurchasedProduct")]
        public async Task<IActionResult> PurchasedProduct(string userId)
        {
            try
            {
                var resultFromBLL = await orderBLL.PurchasedProduct(userId);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.Count; i++)
                {
                    resultFromBLL[i].CartRowVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].CartRowVM.ImageName);
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll(string query, int limit = 10, int currentPage = 1)
        {
            try
            {
                var resultFromBLL = await orderBLL.GetAll(limit, currentPage, query);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var resultFromBLL = await orderBLL.GetbyId(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("changeState/{id}")]
        public async Task<IActionResult> ChangeState(string id, int State)
        {
            try
            {
                var resultFromBLL = await orderBLL.ChangeState(id, State);
                if (resultFromBLL == false)
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
        [HttpGet("AdminGetByState")]
        public async Task<IActionResult> AdminGetByState(int currentPage, int limit, string id, int? state)
        {
            try
            {
                var resultFromBLL = await orderBLL.AdminGetByState(currentPage, limit, id, state);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                //if (resultFromBLL.Count == 0)
                //{
                //    return Ok(new List<OrderVM>());
                //}
                //for (int i = 0; i < resultFromBLL.Count; i++)
                //{
                //    for (int j = 0; j < resultFromBLL[i].OrderDetailVMs.Count; j++)
                //    {
                //        resultFromBLL[i].OrderDetailVMs[j].ProductOrderVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL[i].OrderDetailVMs[j].ProductOrderVM.ImageName);
                //    }
                //}
                return Ok(resultFromBLL);
            }
            catch
            {
                return null;
            }
        }
        [HttpGet("GetFullById")]
        public async Task<IActionResult> GetFullById(string id)
        {
            try
            {
                var resultFromBLL = await orderBLL.GetFullById(id);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                for (int i = 0; i < resultFromBLL.OrderDetailVMs.Count; i++)
                {
                    if (resultFromBLL.OrderDetailVMs[i].ProductOrderVM.ImageName != null)
                    {
                        resultFromBLL.OrderDetailVMs[i].ProductOrderVM.ImageSrc = String.Format("{0}://{1}{2}/Photos/{3}", Request.Scheme, Request.Host, Request.PathBase, resultFromBLL.OrderDetailVMs[i].ProductOrderVM.ImageName);
                    }
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("OrderToday")]
        public async Task<IActionResult> OrderToday(int? state, int currentPage=1, int limit=10)
        {
            try
            {
                var resultFromBLL=await orderBLL.OrderToday(state, currentPage, limit);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }
    
        [HttpGet("orderchart")]
        public async Task<IActionResult> OrderChart(int year)
        {
            try
            {
                var resultFromBLL = await orderBLL.OrderChart(year);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromBLL);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("CompareOrderChart")]
        public async Task<IActionResult> CompareOrderChart(List<int> years)
        {
            try
            {
                var resultFromBLL = await orderBLL.CompareOrderChart(years);
                if (resultFromBLL == null)
                {
                    return BadRequest();
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
