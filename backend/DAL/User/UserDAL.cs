using BO;
using BO.ViewModels.Picture;
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
        //public async Task<List<UserVM>> GetAll()
        //{
        //    var userFromDb = await db.Users.ToListAsync();
        //    if (userFromDb.Count == 0)
        //    {
        //        return new List<UserVM>();
        //    }
        //    var userVMs = userFromDb.Select(x => new UserVM
        //    {
        //        Id = x.Id,
        //        FirstName = x.FirstName,
        //        LastName = x.LastName,
        //        Birthday = x.Birthday,
        //        Email = x.Email,
        //        PhoneNumber = x.PhoneNumber,
        //        Address = x.Address,
        //        Role = x.Role,
        //        Username = x.Username,
        //        Password = x.Password,
        //        CreatedAt = x.CreatedAt,
        //        UpdatedAt = x.UpdatedAt,
        //    }).ToList();
        //    return userVMs;
        //}
        //public async Task<UserVM> GetById(string id)
        //{
        //    var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
        //    if (userFromDb == null)
        //    {
        //        return null;
        //    }
        //    var userVM = new UserVM
        //    {
        //        Id = userFromDb.Id,
        //        FirstName = userFromDb.FirstName,
        //        LastName = userFromDb.LastName,
        //        Birthday = userFromDb.Birthday,
        //        Email = userFromDb.Email,
        //        PhoneNumber = userFromDb.PhoneNumber,
        //        Address = userFromDb.Address,
        //        Role = userFromDb.Role,
        //        Username = userFromDb.Username,
        //        Password = userFromDb.Password,
        //        CreatedAt = userFromDb.CreatedAt,
        //        UpdatedAt = userFromDb.UpdatedAt,
        //    };
        //    return userVM;
        //}
        public async Task<string> GetPasswordById(string id)
        {
            try
            {
                var resultFromDb = await db.Users.Select(x => new { x.Id, x.Password }).SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                return resultFromDb.Password;
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> GetPasswordByUsername(string username)
        {
            try
            {
                var resultFromDb = await db.Users.Select(x => new { x.Username, x.Password }).SingleOrDefaultAsync(x => x.Username == username);
                if (resultFromDb == null)
                {
                    return null;
                }
                return resultFromDb.Password;
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //public async Task<bool> Create(UserVM userVM)
        //{
        //    var user = new BO.Entities.User
        //    {
        //        Id = userVM.Id,
        //        FirstName = userVM.FirstName,
        //        LastName = userVM.LastName,
        //        Birthday = userVM.Birthday,
        //        Email = userVM.Email,
        //        PhoneNumber = userVM.PhoneNumber,
        //        Address = userVM.Address,
        //        Role = userVM.Role,
        //        Username = userVM.Username,
        //        Password = userVM.Password,
        //        CreatedAt = userVM.CreatedAt,
        //        UpdatedAt = userVM.UpdatedAt,
        //    };
        //    await db.Users.AddAsync(user);
        //    var result = await db.SaveChangesAsync();
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public async Task<bool> Update(UserVM userVM)
        //{
        //    var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == userVM.Id);

        //    userFromDb.FirstName = userVM.FirstName;
        //    userFromDb.LastName = userVM.LastName;
        //    userFromDb.Birthday = userVM.Birthday;
        //    userFromDb.Email = userVM.Email;
        //    userFromDb.PhoneNumber = userVM.PhoneNumber;
        //    userFromDb.Address = userVM.Address;
        //    userFromDb.UpdatedAt = userVM.UpdatedAt;

        //    var result = await db.SaveChangesAsync();
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //public async Task<bool> Delete(string id)
        //{
        //    var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
        //    db.Users.Remove(userFromDb);
        //    var result = await db.SaveChangesAsync();
        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        public async Task<UserInfoClientVM> GetByUsername(string username)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Username == username);
            //var picture = await db.Pictures.Where(x=>x.ObjectType=="user").SingleOrDefaultAsync(x => x.ObjectId == userFromDb.Id);
            if (userFromDb == null)
            {
                return null;
            }
            else
            {
                var result = new UserInfoClientVM
                {
                    Id = userFromDb.Id,
                    Username = userFromDb.Username,
                    FirstName = userFromDb.FirstName,
                    LastName = userFromDb.LastName,
                    Birthday = userFromDb.Birthday,
                    Email = userFromDb.Email,
                    PhoneNumber = userFromDb.PhoneNumber,
                    Address = userFromDb.Address,
                    Role = userFromDb.Role,
                    CreatedAt = userFromDb.CreatedAt,
                    ImageName = null,
                    ImageSrc = null,
                };
                return result;

            }

            //var resultFromDb = await db.Users.GroupJoin(
            //    db.Pictures,
            //    u => u.Id,
            //    p => p.ObjectId,
            //    (x, y) => new { u = x, p = y }
            //    ).Where(x=>x.u.Username==username)
            //    .SelectMany(x => x.p.DefaultIfEmpty(), (x, y) => new UserInfoClientVM
            //    {
            //        Id = x.u.Id,
            //        Username = x.u.Username,
            //        FirstName = x.u.FirstName,
            //        LastName = x.u.LastName,
            //        Birthday = x.u.Birthday,
            //        Email = x.u.Email,
            //        PhoneNumber = x.u.PhoneNumber,
            //        Address = x.u.Address,
            //        Role = x.u.Role,
            //        CreatedAt = x.u.CreatedAt,
            //        ImageName = y.Name,
            //        ImageSrc = null,
            //    }).SingleOrDefaultAsync();

            //var resultFromDb = await (from u in db.Users
            //                          join p in db.Pictures on u.Id equals p.ObjectId
            //                          where u.Username == username && p.ObjectId==u.Id
            //                          select new UserInfoClientVM
            //                          {
            //                              Id=u.Id,
            //                              Username = u.Username,
            //                              FirstName=u.FirstName,
            //                              LastName=u.LastName,
            //                              Birthday=u.Birthday,
            //                              Email=u.Email,
            //                              PhoneNumber=u.PhoneNumber,
            //                              Address=u.Address,
            //                              Role=u.Role,
            //                              CreatedAt=u.CreatedAt,
            //                              ImageName=p.Name,
            //                              ImageSrc=null,
            //                          }
            //                       ).SingleOrDefaultAsync();

        }
        public async Task<UserInfoClientVM> GetById(string id)
        {
            //var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            var resultFromDb = await (from u in db.Users
                                      join p in db.Pictures on u.Id equals p.ObjectId into list
                                      from p in list.DefaultIfEmpty()
                                      where u.Id == id
                                      select new UserInfoClientVM
                                      {
                                          Id = u.Id,
                                          Username = u.Username,
                                          FirstName = u.FirstName,
                                          LastName = u.LastName,
                                          Birthday = u.Birthday,
                                          Email = u.Email,
                                          PhoneNumber = u.PhoneNumber,
                                          Address = u.Address,
                                          Role = u.Role,
                                          CreatedAt = u.CreatedAt,
                                          ImageName = p.Name,
                                          ImageSrc = null,
                                      }).SingleOrDefaultAsync();
            //var result = new UserInfoClientVM
            //{
            //    Id = userFromDb.Id,
            //    Username = userFromDb.Username,
            //    FirstName = userFromDb.FirstName,
            //    LastName = userFromDb.LastName,
            //    Birthday = userFromDb.Birthday,
            //    Email = userFromDb.Email,
            //    PhoneNumber = userFromDb.PhoneNumber,
            //    Address = userFromDb.Address,
            //    Role = userFromDb.Role,
            //    CreatedAt = userFromDb.CreatedAt,
            //    ImageName = null,
            //    ImageSrc = null,
            //};
            if (resultFromDb == null)
            {
                return null;
            }
            return resultFromDb;
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
        public async Task<bool> Edit(UserVM userVM, PictureVM picVM)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == userVM.Id);

            userFromDb.LastName = userVM.LastName;
            userFromDb.FirstName = userVM.FirstName;
            userFromDb.Email = userVM.Email;
            userFromDb.PhoneNumber = userVM.PhoneNumber;
            userFromDb.Address = userVM.Address;
            userFromDb.Birthday = userVM.Birthday;
            userFromDb.UpdatedAt = DateTime.Now;

            var resultUser = await db.SaveChangesAsync();
            if (resultUser == 0)
            {
                return false;
            }

            if (picVM.Name != null)
            {
                var pic = await db.Pictures.SingleOrDefaultAsync(x => x.ObjectId == userVM.Id);
                if (pic == null)
                {
                    var picture = new BO.Entities.Picture
                    {
                        Id = picVM.Id,
                        Name = picVM.Name,
                        Published = picVM.Published,
                        ObjectType = picVM.ObjectType,
                        ObjectId = picVM.ObjectId
                    };
                    await db.Pictures.AddAsync(picture);
                    var resultPic = await db.SaveChangesAsync();
                    if (resultPic == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    pic.Name = picVM.Name;
                    var resultPic = await db.SaveChangesAsync();
                    if (resultPic == 0)
                    {
                        return false;
                    }
                }
            }


            return true;
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
