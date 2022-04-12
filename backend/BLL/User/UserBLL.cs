using BO.ViewModels.User;
using DAL.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
                RoleId = model.RoleId,
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
            userVM.RoleId = model.RoleId;
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
    }
}
