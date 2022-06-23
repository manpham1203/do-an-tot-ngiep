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

        public async Task<string> GetPasswordById(string id)
        {
            try
            {
                var resultFromDb = await db.Users.Select(x => new { x.Id, x.Password })
                    .SingleOrDefaultAsync(x => x.Id == id);
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
                var resultFromDb = await db.Users.Select(x => new { x.Username, x.Password })
                    .SingleOrDefaultAsync(x => x.Username == username);
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
                    Published=userFromDb.Published,
                    Deleted=userFromDb.Deleted,
                };
                return result;

            }


        }
        public async Task<bool> CheckUsername(string username)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Username == username);
            if (userFromDb == null)
            {
                return false;
            }
            return true;

        }
        public async Task<bool> CheckEmail(string email)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Email == email);
            if (userFromDb == null)
            {
                return false;
            }
            return true;

        }
        public async Task<bool> CheckPhone(string phone)
        {
            var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.PhoneNumber == phone);
            if (userFromDb == null)
            {
                return false;
            }
            return true;
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
                Published = userVM.Published,
                Deleted = userVM.Deleted,
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
            userFromDb.UpdatedAt = userVM.UpdatedAt;

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

        public async Task<List<UserVM>> GetListBirthday(int? type)
        {
            try
            {
                var dateToday = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
                var dateStart = dateToday.AddDays(-10);
                var dateEnd = dateToday.AddDays(+30);

                var resultFromDb = await db.Users
                    .Where(x => x.Birthday.Value.DayOfYear > dateStart.DayOfYear
                    && x.Birthday.Value.DayOfYear < dateEnd.DayOfYear)
                    .ToListAsync();

                if (resultFromDb.Count == 0)
                {
                    return new List<UserVM>();
                }

                if (type.HasValue)
                {
                    switch (type)
                    {
                        case 1:
                            resultFromDb = resultFromDb
                                .Where(x => x.Birthday.Value.DayOfYear >= dateStart.DayOfYear
                                && x.Birthday.Value.DayOfYear < dateToday.DayOfYear)
                                .ToList();
                            break;
                        case 2:
                            resultFromDb = resultFromDb.Where(x => x.Birthday.Value.Day == dateToday.Day
                            && x.Birthday.Value.Month == dateToday.Month).ToList();
                            break;
                        case 3:
                            resultFromDb = resultFromDb
                                .Where(x => x.Birthday.Value.DayOfYear > dateToday.DayOfYear
                                && x.Birthday.Value.DayOfYear <= dateEnd.DayOfYear).ToList();
                            break;

                        default: break;
                    }
                }
                var result = resultFromDb.Select(x => new UserVM
                {
                    Id = x.Id,
                    Username = x.Username,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Birthday = x.Birthday,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    Role = x.Role,
                    CreatedAt = x.CreatedAt,
                    Image = null,
                    ImageSrc = null,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<string>> ListUserId(bool deleted)
        {
            try
            {
                return await db.Users.Where(x => x.Deleted == deleted).Select(x => x.Id).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<UserVM> UserDetail(string id)
        {
            try
            {
                var resultFromDb = await (from u in db.Users
                                          join p in db.Pictures on u.Id equals p.ObjectId into list
                                          from p in list.DefaultIfEmpty()
                                          where u.Id == id
                                          select new UserVM
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
                                              Image = p.Name,
                                              ImageSrc = null,
                                              Published = u.Published,
                                              Deleted = u.Deleted
                                          }).SingleOrDefaultAsync();
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Published(string id)
        {
            try
            {
                var resultFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                resultFromDb.Published = !resultFromDb.Published;
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Deleted(string id)
        {
            try
            {
                var resultFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                resultFromDb.Deleted = !resultFromDb.Deleted;
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(string id)
        {
            var postFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == id);
            db.Users.Remove(postFromDb);
            var result = await db.SaveChangesAsync();
            if (result == 0)
            {
                return false;
            }
            var pictureFromDb = await db.Pictures.Where(x => x.ObjectId == id).Where(x => x.ObjectType == "user").SingleOrDefaultAsync();
            if (pictureFromDb != null)
            {
                db.Pictures.Remove(pictureFromDb);
                var resultPicture = await db.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> Update(UserVM userVM, PictureVM picVM)
        {
            try
            {
                var userFromDb = await db.Users.SingleOrDefaultAsync(x => x.Id == userVM.Id);

                userFromDb.LastName = userVM.LastName;
                userFromDb.FirstName = userVM.FirstName;
                userFromDb.Email = userVM.Email;
                userFromDb.PhoneNumber = userVM.PhoneNumber;
                userFromDb.Address = userVM.Address;
                userFromDb.Birthday = userVM.Birthday;
                userFromDb.UpdatedAt = DateTime.Now;
                userFromDb.Published = userVM.Published;

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
            catch
            {
                return false;
            }
        }
    
        
    }
}
