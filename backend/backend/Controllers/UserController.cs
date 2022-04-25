using BLL.User;
using BO.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserBLL userBLL;
        public UserController()
        {
            userBLL = new UserBLL();
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var userVMs = await userBLL.GetAll();
        //        return Ok(userVMs);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    try
        //    {
        //        var user = await userBLL.GetById(id);
        //        if (user == null)
        //        {
        //            return NotFound(); ;
        //        }
        //        return Ok(user);
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create(CreateUserVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var userCreate = await userBLL.Create(model);
        //            if (userCreate == false)
        //            {
        //                return BadRequest();
        //            }
        //            return StatusCode(StatusCodes.Status201Created);
        //        }
        //        catch
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, UpdateUserVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var userUpdate = await userBLL.Update(id, model);
        //            if (userUpdate == false)
        //            {
        //                return BadRequest();
        //            }
        //            return Ok();
        //        }
        //        catch
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    try
        //    {
        //        var userDelete=await userBLL.Delete(id);
        //        if(userDelete == false)
        //        {
        //            return BadRequest();
        //        }
        //        return Ok();
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userBLL.Register(model);
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
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(string id, UpdateUserVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await userBLL.Edit(id, model);
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
                    var user = await userBLL.ChangePass(id, model);
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
                var user = await userBLL.Login(model);
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
                var user = await userBLL.FindUsername(username);
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
