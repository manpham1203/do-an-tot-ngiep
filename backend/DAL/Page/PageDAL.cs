using BO;
using BO.ViewModels.Page;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Page
{
    public class PageDAL
    {
        private AppDbContext db;
        public PageDAL()
        {
            db= new AppDbContext();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                var resultFromDb = await db.Pages.SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<bool> Create(PageVM model)
        {
            try
            {
                var obj = new BO.Entities.Page
                {
                    Id = model.Id,
                    Title = model.Title,
                    Slug = model.Slug,
                    Content = model.Content,
                    Type=model.Type,
                    Published=model.Published,
                    Deleted=model.Deleted,
                    CreatedAt=model.CreatedAt,
                    UpdatedAt=model.UpdatedAt,
                };
                await db.Pages.AddAsync(obj);
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
        public async Task<List<string>> GetListId(bool deleted)
        {
            try
            {
                return await db.Pages.Where(x => x.Deleted == deleted).Select(x => x.Id).ToListAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<PageVM> GetById(string id)
        {
            try
            {
                var resultFromDb = await db.Pages.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                return new PageVM
                {
                    Id = resultFromDb.Id,
                    Title = resultFromDb.Title,
                    Slug = resultFromDb.Slug,
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
                var resultFromDb = await db.Pages.SingleOrDefaultAsync(x => x.Id == id);
                db.Pages.Remove(resultFromDb);
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
                var resultFromDb = await db.Pages.SingleOrDefaultAsync(x => x.Id == id);
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
                var resultFromDb = await db.Pages.SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<bool> Update(PageVM model)
        {
            try
            {
                var resultFromDb = await db.Pages.SingleOrDefaultAsync(x => x.Id == model.Id);
                resultFromDb.Title = model.Title;
                resultFromDb.Slug = model.Slug;
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
        public async Task<List<PageVM>> GetListByType(string type)
        {
            try
            {
                var resultFromDb = await db.Pages.Where(x => x.Deleted == false && x.Published == true && x.Type == type).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<PageVM>();
                }
                return resultFromDb.Select(x => new PageVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    Content = x.Content,
                    Type = x.Type,
                    Published = x.Published,
                    Deleted = x.Deleted,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,

                }).ToList();
            }
            catch
            {
                return null;
            }
        }
        public async Task<PageVM> GetBySlug(string slug)
        {
            try
            {
                var resultFromDb = await db.Pages.FirstOrDefaultAsync(x => x.Slug == slug);
                if (resultFromDb == null)
                {
                    return null;
                }
                return new PageVM
                {
                    Id = resultFromDb.Id,
                    Title = resultFromDb.Title,
                    Slug = resultFromDb.Slug,
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
    
    }
}
