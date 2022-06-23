using BLL.Contact;
using BO.ViewModels.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactBLL contactBLL;
        public ContactController()
        {
            contactBLL = new ContactBLL();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ContactVM model)
        {
            try
            {
                var resultFromBLL=await contactBLL.Create(model);
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
    
        [HttpGet("contactpagination")]
        public async Task<IActionResult> ContactPagination(bool deleted, int limit=10, int currentPage=1)
        {
            try
            {
                var resultFromBLL=await contactBLL.ContactPagination(deleted, limit, currentPage);
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
                var resultFromBLL = await contactBLL.GetById(id);
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
                var resultFromBLL = await contactBLL.Delete(id);
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
                var resultFromBLL = await contactBLL.Published(id);
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
                var resultFromBLL = await contactBLL.Deleted(id);
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
        public async Task<IActionResult> Update(string id, ContactVM model)
        {
            try
            {
                var resultFromBLL = await contactBLL.Update(id, model);
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
                var resultFromBLL = await contactBLL.GetList();
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
