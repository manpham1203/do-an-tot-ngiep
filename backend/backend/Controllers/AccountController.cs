using BLL.User;
using BO.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountBLL accountBLL;
        public AccountController()
        {
            accountBLL = new AccountBLL();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await accountBLL.Register(model);
                    switch (user)
                    {
                        case "fail":
                            return BadRequest();
                        case "pass":
                            return Content("pass");
                        case "exists":
                            return Content("exists");
                        case "done":
                            return StatusCode(StatusCodes.Status201Created);
                        default:
                            return BadRequest();
                    }
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateUserVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await accountBLL.Update(id,model);
                    if (user)
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("changepass/{id}")]
        public async Task<IActionResult> ChangePass(string id, ChangePassVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await accountBLL.ChangePass(id, model);
                    if (user)
                    {
                        return Ok();
                    }
                    return BadRequest();
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginVM model)
        {

            try
            {
                var user = await accountBLL.Login(model);
                if (user != null)
                {
                    return Ok(user);
                }
                return Content("notfound");
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost("findUsername")]
        public async Task<IActionResult> FindUsername(string username)
        {

            try
            {
                var user = await accountBLL.FindUsername(username);
                if (user)
                {
                    return Ok(user);
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
