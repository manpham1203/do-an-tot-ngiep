using BO;
using BO.ViewModels.BrandImage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BrandImage
{
    public class BrandImageDAL
    {
        private readonly AppDbContext db;
        public BrandImageDAL()
        {
            db = new AppDbContext();
        }
        public async Task<BrandImageVM> GetById(string id)
        {
            try
            {
                var imgFromDb = await db.BrandImages.SingleOrDefaultAsync(b => b.Id == id);
                if (imgFromDb == null)
                {
                    return null;
                }
                var brandImageVM = new BrandImageVM();

                brandImageVM.Id = imgFromDb.Id;
                brandImageVM.Name = imgFromDb.Name;
                brandImageVM.BrandId = imgFromDb.BrandId;
                brandImageVM.Pulished = imgFromDb.Pulished;

                return brandImageVM;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<BrandImageVM>> GetByBrandId(string id)
        {
            var imgFromDb = await db.BrandImages.Where(b => b.BrandId == id).ToListAsync();
            if (imgFromDb == null)
            {
                return new List<BrandImageVM>();
            }
            var brandImgVMs = imgFromDb.Select(x => new BrandImageVM
            {
                Id = x.Id,
                Name = x.Name,
                BrandId=x.BrandId,
                Pulished=x.Pulished,
            }).ToList();

            return brandImgVMs;
        }

        public async Task<bool> Create(List<BrandImageVM> obj)
        {
            var imgs = obj.Select(x => new BO.Entities.BrandImage
            {
                Id = x.Id,
                Name = x.Name,
                BrandId = x.BrandId,
                Pulished = x.Pulished,
            });
            foreach (var img in imgs)
            {
                await db.BrandImages.AddAsync(img);
            }
            var result = await db.SaveChangesAsync();
            if (result >= imgs.Count())
            {
                return true;
            }
            return false;
        }
    }
}
