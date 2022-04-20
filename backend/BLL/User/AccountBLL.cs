using BO.ViewModels.User;
using DAL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BLL.User
{
    public class AccountBLL
    {
        private AccountDAL accountDAL;
        private UserBLL userBLL;
        private CommonBLL cm;
        public AccountBLL()
        {
            accountDAL = new AccountDAL();
        }
        
    }
}
