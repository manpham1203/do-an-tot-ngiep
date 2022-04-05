using BO;
using BO.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.User
{
    public class AccountDAL
    {
        private readonly AppDbContext db;
        public AccountDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> Register(UserVM userVM)
        {
            var user = new BO.Entities.User
            {
                Id = userVM.Id,
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Birthday = userVM.Birthday,
                Email = userVM.Email,
                PhoneNumber = userVM.PhoneNumber,
                Address = userVM.Address,
                RoleId = userVM.RoleId,
                Username = userVM.Username,
                Password = userVM.Password,
                Avatar=null,
                CreatedAt = userVM.CreatedAt,
                UpdatedAt = userVM.UpdatedAt,
            };
            await db.Users.AddAsync(user);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(UserVM userVM)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == userVM.Id);

            userFromDb.LastName = userVM.LastName;
            userFromDb.FirstName = userVM.FirstName;
            userFromDb.Email = userVM.Email;
            userFromDb.PhoneNumber = userVM.PhoneNumber;
            userFromDb.Address = userVM.Address;
            userFromDb.Birthday = userVM.Birthday;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> ChangePass(UserVM userVM)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == userVM.Id);

            userFromDb.Password = userVM.Password;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
