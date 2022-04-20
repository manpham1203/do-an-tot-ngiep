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
        }
}
