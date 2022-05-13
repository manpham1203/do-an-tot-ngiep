using BO;
using BO.ViewModels.Contact;
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
    }
}
