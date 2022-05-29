using BLL.Contact;
using BO.ViewModels.Contact;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private QuestionBLL questionBLL;
        public QuestionController()
        {
            questionBLL = new QuestionBLL();
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(QuestionVM model)
        {
            try
            {
                var resultFromBLL=await questionBLL.Create(model);
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
        [HttpGet("questionpagination")]
        public async Task<IActionResult> QuestionPagination(string email, string name, string content, int currentPage=1, int limit =10)
        {
            try
            {
                var resultFromBLL=await questionBLL.QuestionPagination(currentPage, limit, email, name, content);
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

        [HttpGet("QuestionToday")]
        public async Task<IActionResult> QuestionToday(string email, string name, string content, int currentPage = 1, int limit = 10)
        {
            try
            {
                var resultFromBLL = await questionBLL.QuestionToday(currentPage, limit, email, name, content);
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
