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
        [HttpPost("create")]
        public async Task<IActionResult> Create(ContactVM model)
        {
            try
            {
                var resultFromBLL=await contactBLL.Create(model);
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
        [HttpGet("contactpagination")]
        public async Task<IActionResult> ContactPagination(string email, string name, string content, int currentPage=1, int limit =10)
        {
            try
            {
                var resultFromBLL=await contactBLL.ContactPagination(currentPage, limit, email, name, content);
                if (resultFromBLL == null)
                {
                    return BadRequest();
                }
                if (resultFromBLL.TotalResult >= 0)
                {
                    return Ok(resultFromBLL);
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
