using BO.ViewModels.User;
using DAL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace BLL.User
{
    public class UserBLL
    {
        private readonly UserDAL userDAL;
        private CommonBLL cm;
        public UserBLL()
        {
            userDAL = new UserDAL();
        }
        public async Task<List<UserVM>> GetAll()
        {
            return await userDAL.GetAll();
        }
        public async Task<UserVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            return await userDAL.GetById(id);
        }
        public async Task<bool> Create(CreateUserVM model)
        {
            cm=new CommonBLL();
            var userId = cm.RandomString(9);
            var checkExists=await GetById(userId);
            while (checkExists != null)
            {
                userId=cm.RandomString(9);
                checkExists = await GetById(userId);
            }
            userId = "USR" + userId;
            var userVM = new UserVM
            {
                Id=userId,
                FirstName=model.FirstName,
                LastName=model.LastName,
                Birthday=model.Birthday,
                Email=model.Email,
                PhoneNumber=model.PhoneNumber,
                Address=model.Address,
                Role = model.Role,
                Username=model.Username,
                Password=model.Password,
                CreatedAt=DateTime.Now,
                UpdatedAt=null,
            };
            return await userDAL.Create(userVM);
        }
        public async Task<bool> Update(string id, UpdateUserVM model)
        {
            var checkExists = await GetById(id);
            if (checkExists == null)
            {
                return false;
            }

            var userVM = new UserVM();
            userVM.Id = id;
            userVM.FirstName=model.FirstName;
            userVM.LastName=model.LastName;
            userVM.Birthday=model.Birthday;
            userVM.Email=model.Email;
            userVM.PhoneNumber=model.PhoneNumber;
            userVM.Address=model.Address;
            userVM.Role = model.Role;
            userVM.Username=model.Username;
            userVM.Password=model.Password;
            userVM.UpdatedAt=DateTime.Now;

            return await userDAL.Update(userVM);
        }
        public async Task<bool> Delete(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var userVM = await GetById(id);
            if (userVM == null)
            {
                return false;
            }
            return await userDAL.Delete(id);
        }
        public async Task<UserVM> GetByUsername(string username)
        {
            return await userDAL.GetByUsername(username);
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
            var checkUsername = await GetByUsername(username);
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
            var checkUsername = await GetByUsername(model.Username);
            if (checkUsername != null)
            {
                return "exists";
            }
            cm = new CommonBLL();
            var userId = cm.RandomString(12);
            var checkExists = await GetById(userId);
            while (checkExists != null)
            {
                userId = cm.RandomString(12);
                checkExists = await GetById(userId);
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
                Role = 0,
                Username = model.Username.ToLower(),
                Password = ToSHA256(model.Password),
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
            };
            var result = await userDAL.Register(userVM);
            if (result)
            {
                return "done";
            }
            return "fail";
        }
        public async Task<UserVM> Login(LoginVM model)
        {

            model.Username.ToLower();
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return null;
            }
            var user = await GetByUsername(model.Username);
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
        public async Task<bool> Edit(string id, UpdateUserVM model)
        {
            var checkExists = await GetById(id);
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
            userVM.Role = 0;
            userVM.Username = null;
            userVM.Password = null;
            userVM.UpdatedAt = DateTime.Now;

            return await userDAL.Edit(userVM);
        }
        public async Task<bool> ChangePass(string id, ChangePassVM model)
        {
            if (model.NewPassword != model.RePassword)
            {
                return false;
            }
            var checkExists = await GetById(id);
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

            return await userDAL.ChangePass(userVM);
        }
    }
}
