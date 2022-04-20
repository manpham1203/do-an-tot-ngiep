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
    public class UserDAL
    {
        private readonly AppDbContext db;
        public UserDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<UserVM>> GetAll()
        {
            var userFromDb = await db.Users.ToListAsync();
            if (userFromDb.Count == 0)
            {
                return new List<UserVM>();
            }
            var userVMs = userFromDb.Select(x => new UserVM
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Birthday = x.Birthday,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Role = x.Role,
                Username = x.Username,
                Password = x.Password,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
            }).ToList();
            return userVMs;
        }
        public async Task<UserVM> GetById(string id)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            if (userFromDb == null)
            {
                return null;
            }
            var userVM = new UserVM
            {
                Id = userFromDb.Id,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Birthday = userFromDb.Birthday,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                Address = userFromDb.Address,
                Role = userFromDb.Role,
                Username = userFromDb.Username,
                Password = userFromDb.Password,
                CreatedAt = userFromDb.CreatedAt,
                UpdatedAt = userFromDb.UpdatedAt,
            };
            return userVM;
        }
        public async Task<bool> Create(UserVM userVM)
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
                Role = userVM.Role,
                Username = userVM.Username,
                Password = userVM.Password,
                CreatedAt = userVM.CreatedAt,
                UpdatedAt = userVM.UpdatedAt,
            };
            await db.Users.AddAsync(user);
            var result =await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(UserVM userVM)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x=>x.Id==userVM.Id);

            userFromDb.FirstName=userVM.FirstName;
            userFromDb.LastName=userVM.LastName;
            userFromDb.Birthday = userVM.Birthday;
            userFromDb.Email=userVM.Email;
            userFromDb.PhoneNumber=userVM.PhoneNumber;
            userFromDb.Address=userVM.Address;
            userFromDb.UpdatedAt=userVM.UpdatedAt;

            var result= await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            db.Users.Remove(userFromDb);
            var result= await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<UserVM> GetByUsername(string username)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (userFromDb == null)
            {
                return null;
            }
            var userVM = new UserVM
            {
                Id = userFromDb.Id,
                FirstName = userFromDb.FirstName,
                LastName = userFromDb.LastName,
                Birthday = userFromDb.Birthday,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                Address = userFromDb.Address,
                Role = userFromDb.Role,
                Username = userFromDb.Username,
                Password = userFromDb.Password,
                CreatedAt = userFromDb.CreatedAt,
                UpdatedAt = userFromDb.UpdatedAt,
            };
            return userVM;
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
                Role = userVM.Role,
                Username = userVM.Username,
                Password = userVM.Password,
                Avatar = null,
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
        public async Task<bool> Edit(UserVM userVM)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == userVM.Id);

            userFromDb.LastName = userVM.LastName;
            userFromDb.FirstName = userVM.FirstName;
            userFromDb.Email = userVM.Email;
            userFromDb.PhoneNumber = userVM.PhoneNumber;
            userFromDb.Address = userVM.Address;
            userFromDb.Birthday = userVM.Birthday;
            userFromDb.UpdatedAt = DateTime.Now;

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
            userFromDb.UpdatedAt = DateTime.Now;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
