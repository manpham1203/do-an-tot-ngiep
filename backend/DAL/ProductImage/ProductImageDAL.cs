using BO;
using BO.ViewModels.ProductImage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ProductImage
{
    public class ProductImageDAL
    {
        private readonly AppDbContext db;
        public ProductImageDAL()
        {
            db = new AppDbContext();
        }
        public async Task<ProductImageVM> GetById(string id)
        {
            try
            {
                var imgFromDb = await db.ProductImages.SingleOrDefaultAsync(b => b.Id == id);
                if (imgFromDb == null)
                {
                    return null;
                }
                var productImageVM = new ProductImageVM();

                productImageVM.Id = imgFromDb.Id;
                productImageVM.Name = imgFromDb.Name;
                productImageVM.ProductId = imgFromDb.ProductId;
                productImageVM.Pulished = imgFromDb.Pulished;

                return productImageVM;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<ProductImageVM>> GetByProductId(string id)
        {
            var imgFromDb = await db.ProductImages.Where(b => b.ProductId == id).ToListAsync();
            if (imgFromDb == null)
            {
                return new List<ProductImageVM>();
            }
            var productImageVMs = imgFromDb.Select(x => new ProductImageVM
            {
                Id = x.Id,
                Name = x.Name,
                ProductId = x.ProductId,
                Pulished = x.Pulished,
            }).ToList();

            return productImageVMs;
        }

        public async Task<bool> Create(List<ProductImageVM> obj)
        {
            var imgs = obj.Select(x => new BO.Entities.ProductImage
            {
                Id = x.Id,
                Name = x.Name,
                ProductId = x.ProductId,
                Pulished = x.Pulished,
            });
            foreach (var img in imgs)
            {
                await db.ProductImages.AddAsync(img);
            }
            var result = await db.SaveChangesAsync();
            if (result >= imgs.Count())
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var imgFormDb = await db.ProductImages.SingleOrDefaultAsync(x => x.Id == id);
            if (imgFormDb == null)
            {
                return false;
            }
            db.ProductImages.Remove(imgFormDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Pulished(string id, bool pulished)
        {
            var productImage = await db.ProductImages.SingleOrDefaultAsync(x => x.Id == id);

            productImage.Pulished = pulished;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

    }
}
