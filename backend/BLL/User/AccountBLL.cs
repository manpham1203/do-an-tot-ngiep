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
        public static string ToSHA256(string s)
        {
            var str = Encoding.ASCII.GetBytes(s);
            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(str);
            var newStr = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                newStr.Append(hash[i].ToString("x2"));
            }
            return newStr.ToString();
        }
        public async Task<bool> FindUsername(string username)
        {
            userBLL = new UserBLL();
            var checkUsername = await userBLL.GetByUsername(username);
            if (checkUsername != null)
            {
                return true;
            }
            return false;
        }

        public async Task<string> Register(RegisterVM model)
        {          
            if (model.Password != model.RePassword)
            {
                return "fail";
            }
            userBLL=new UserBLL();
            var checkUsername = await userBLL.GetByUsername(model.Username);
            if(checkUsername != null)
            {
                return "exists";
            }
            cm = new CommonBLL();
            var userId = cm.RandomString(12);
            var checkExists = await userBLL.GetById(userId);
            while (checkExists != null)
            {
                userId = cm.RandomString(12);
                checkExists = await userBLL.GetById(userId);
            }
            
           
            var userVM = new UserVM
            {
                Id = userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Birthday = null,
                Email = null,
                PhoneNumber = null,
                Address = null,
                RoleId = 0,
                Username = model.Username.ToLower(),
                Password = ToSHA256(model.Password),
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
            };
            var result= await accountDAL.Register(userVM);
            if (result)
            {
                return "done";
            }
            return "fail";
        }
        public async Task<UserVM> Login(LoginVM model)
        {

            model.Username.ToLower();
            if(string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return null;
            }
            userBLL=new UserBLL();
            var user = await userBLL.GetByUsername(model.Username);
            if (user == null)
            {
                return null;
            }
            
            if (ToSHA256(model.Password) != user.Password)
            {
                return null;
            }
           
            return user;
        }
        public async Task<bool> Update(string id, UpdateUserVM model)
        {
            userBLL = new UserBLL();
            var checkExists = await userBLL.GetById(id);
            if (checkExists == null)
            {
                return false;
            }

            var userVM = new UserVM();
            userVM.Id = id;
            userVM.FirstName = model.FirstName;
            userVM.LastName = model.LastName;
            userVM.Birthday = model.Birthday;
            userVM.Email = model.Email;
            userVM.PhoneNumber = model.PhoneNumber;
            userVM.Address = model.Address;
            userVM.RoleId = 0;
            userVM.Username = null;
            userVM.Password = null;
            userVM.UpdatedAt = DateTime.Now;

            return await accountDAL.Update(userVM);
        }
        public async Task<bool> ChangePass(string id, ChangePassVM model)
        {
            if (model.NewPassword != model.RePassword)
            {
                return false;
            }

            userBLL = new UserBLL();
            var checkExists = await userBLL.GetById(id);
            if (checkExists == null)
            {
                return false;
            }
            
            if (checkExists.Password != ToSHA256(model.OldPassword))
            {
                return false;
            }
            

            var userVM = new UserVM();
            userVM.Id = id;
            userVM.Password = ToSHA256(model.NewPassword);
            userVM.UpdatedAt = DateTime.Now;

            return await accountDAL.ChangePass(userVM);
        }
    }
}
