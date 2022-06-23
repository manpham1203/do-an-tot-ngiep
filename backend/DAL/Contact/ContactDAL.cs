using BO;
using BO.ViewModels.Contact;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contact
{
    public class ContactDAL
    {
        private AppDbContext db;
        public ContactDAL()
        {
            db = new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Contacts.SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<List<string>> GetListId(bool deleted)
        {
            try
            {
                return await db.Contacts.Where(x => x.Deleted == deleted).Select(x => x.Id).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Create(ContactVM model)
        {
            try
            {
                var obj = new BO.Entities.Contact
                {
                    Id = model.Id,
                    Content = model.Content,
                    Type = model.Type,
                    Published = model.Published,
                    Deleted = model.Deleted,
                    CreatedAt = model.CreatedAt,
                    UpdatedAt = model.UpdatedAt,
                };
                await db.Contacts.AddAsync(obj);
                var result = await db.SaveChangesAsync();
                if (result == 0)
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
        public async Task<ContactVM> GetById(string id)
        {
            try
            {
                var resultFromDb = await db.Contacts.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                return new ContactVM
                {
                    Id = resultFromDb.Id,
                    Content = resultFromDb.Content,
                    Type = resultFromDb.Type,
                    Published = resultFromDb.Published,
                    Deleted = resultFromDb.Deleted,
                    CreatedAt = resultFromDb.CreatedAt,
                    UpdatedAt = resultFromDb.UpdatedAt,
                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Delete(string id)
        {
            try
            {
                var resultFromDb = await db.Contacts.SingleOrDefaultAsync(x => x.Id == id);
                db.Contacts.Remove(resultFromDb);
                var result = await db.SaveChangesAsync();
                if (result == 0)
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
        public async Task<bool> Published(string id)
        {
            try
            {
                var resultFromDb = await db.Contacts.SingleOrDefaultAsync(x => x.Id == id);
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
                var resultFromDb = await db.Contacts.SingleOrDefaultAsync(x => x.Id == id);
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

        public async Task<bool> Update(ContactVM model)
        {
            try
            {
                var resultFromDb = await db.Contacts.SingleOrDefaultAsync(x => x.Id == model.Id);
                resultFromDb.Content = model.Content;
                resultFromDb.Published = model.Published;
                resultFromDb.UpdatedAt = model.UpdatedAt;
                resultFromDb.Type = model.Type;
                var result = await db.SaveChangesAsync();
                if (result == 0)
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
        public async Task<List<ContactVM>> GetListByType(string type)
        {
            try
            {
                var resultFromDb = await db.Contacts.Where(x => x.Deleted == false && x.Published == true && x.Type == type).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<ContactVM>();
                }
                return resultFromDb.Select(x => new ContactVM
                {
                    Id=x.Id,
                    Content=x.Content,
                    Type=x.Type,
                    Published=x.Published,
                    Deleted=x.Deleted,
                    CreatedAt=x.CreatedAt,
                    UpdatedAt=x.UpdatedAt,
                    
                }).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
