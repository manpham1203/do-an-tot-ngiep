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
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(ContactVM model)
        {
            try
            {
                var obj = new BO.Entities.Contact
                {
                    Id = model.Id,
                    Name = model.Name,
                    Content = model.Content,
                    CreatedAt = model.CreatedAt,
                    Email = model.Email,
                };
                await db.Contacts.AddAsync(obj);
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
        public async Task<List<ContactVM>> GetAll()
        {
            try
            {
                var resultFromDb = await db.Contacts.OrderByDescending(x => x.CreatedAt).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<ContactVM>();
                }
                var result = resultFromDb.Select(x => new ContactVM
                {
                    Id = x.Id,
                    Name=x.Name,
                    Content=x.Content,
                    Email=x.Email,
                    CreatedAt = x.CreatedAt,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    
        public async Task<List<ContactVM>> ContactToday()
        {
            try
            {
                var resultFromDb = await db.Contacts
                    .Where(x => x.CreatedAt.Day == DateTime.Today.Day 
                        && x.CreatedAt.Month == DateTime.Today.Month 
                        && x.CreatedAt.Year == DateTime.Today.Year)
                    .OrderByDescending(x => x.CreatedAt).ToListAsync();
                return resultFromDb.Select(x => new ContactVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Content = x.Content,
                    Email = x.Email,
                    CreatedAt = x.CreatedAt,
                }).ToList();
            }
            catch
            {
                return null;
            }
        }
    }
}
