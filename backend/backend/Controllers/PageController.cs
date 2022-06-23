
using BLL.Page;
using BO.ViewModels.Page;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private PageBLL pageBLL;
        public PageController()
        {
            pageBLL = new PageBLL();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PageVM pageVM)
        {
            try
            {
                var resultFromBLL=await pageBLL.Create(pageVM);
                if (resultFromBLL == false)
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
        [HttpGet("pagepagination")]
        public async Task<IActionResult> PagePagination(bool deleted, int limit = 10, int currentPage = 1)
        {
            try
            {
                var resultFromBLL = await pageBLL.PagePagination(deleted, limit, currentPage);
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
                var resultFromBLL = await pageBLL.GetById(id);
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

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var resultFromBLL = await pageBLL.Delete(id);
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
        [HttpPut("published")]
        public async Task<IActionResult> Published(string id)
        {
            try
            {
                var resultFromBLL = await pageBLL.Published(id);
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
        [HttpPut("deleted")]
        public async Task<IActionResult> Deleted(string id)
        {
            try
            {
                var resultFromBLL = await pageBLL.Deleted(id);
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, PageVM model)
        {
            try
            {
                var resultFromBLL = await pageBLL.Update(id, model);
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

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var resultFromBLL = await pageBLL.GetList();
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
    
        [HttpGet("GetBySlug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            try
            {
                var resultFromDb = await pageBLL.GetBySlug(slug);
                if(resultFromDb == null)
                {
                    return BadRequest();
                }
                return Ok(resultFromDb);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
