﻿using BO;
using BO.ViewModels.Brand;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Brand
{
    public class BrandDAL
    {
        private readonly AppDbContext db;
        public BrandDAL()
        {
            db = new AppDbContext();
        }
        //public BrandDAL() { }
        //public BrandDAL(AppDbContext context)
        //{
        //    db= context;
        //}
        public async Task<List<BrandVM>> GetAll()
        {
            var brandFromDb = await db.Brands.ToListAsync();

            if(brandFromDb.Count==0)
            {
                return new List<BrandVM>();
            }

            var brandVMs = brandFromDb.Select(x => new BrandVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Pulished = x.Pulished,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
            }).ToList();
            return brandVMs;

        }
        public async Task<BrandVM> GetById(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(b => b.Id == id);
            if (brandFromDb == null)
            {
                return null;
            }
            var brandVM = new BrandVM();

            brandVM.Id = brandFromDb.Id;
            brandVM.Name = brandFromDb.Name;
            brandVM.Slug = brandFromDb.Slug;
            brandVM.FullDescription = brandFromDb.FullDescription;
            brandVM.ShortDescription = brandFromDb.ShortDescription;
            brandVM.Pulished = brandFromDb.Pulished;
            brandVM.Deleted = brandFromDb.Deleted;
            brandVM.CreatedAt = brandFromDb.CreatedAt;
            brandVM.UpdatedAt = brandFromDb.UpdatedAt;
            brandVM.Ordinal = brandFromDb.Ordinal;

            return brandVM;
        }
        public async Task<bool> Create(BrandVM brandVM)
        {
            var brand = new BO.Entities.Brand
            {
                Id = brandVM.Id,
                Name = brandVM.Name,
                Slug = brandVM.Slug,
                FullDescription = brandVM.FullDescription,
                ShortDescription = brandVM.ShortDescription,
                Pulished = brandVM.Pulished,
                Deleted = brandVM.Deleted,
                CreatedAt = brandVM.CreatedAt,
                UpdatedAt = brandVM.UpdatedAt,
            };
            await db.Brands.AddAsync(brand);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(BrandVM brandVM)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == brandVM.Id);

            brandFromDb.Name = brandVM.Name;
            brandFromDb.Slug = brandVM.Slug;
            brandFromDb.FullDescription = brandVM.FullDescription;
            brandFromDb.ShortDescription = brandVM.ShortDescription;
            brandFromDb.Pulished = brandVM.Pulished;
            brandFromDb.Deleted = brandVM.Deleted;
            brandFromDb.UpdatedAt = brandVM.UpdatedAt;
            brandFromDb.Ordinal = brandVM.Ordinal;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
            db.Brands.Remove(brandFromDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Pulished(string id, bool pulished)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);

            brandFromDb.Pulished = pulished;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id, bool deleted)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);

            brandFromDb.Deleted = deleted;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<BrandVM>> GetAllBrandDeleted(bool deleted)
        {
            var brandFromDb = await db.Brands.Where(x=>x.Deleted== deleted).ToListAsync();

            if (brandFromDb.Count == 0)
            {
                return new List<BrandVM>();
            }

            var brandVMs = brandFromDb.Select(x => new BrandVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Pulished = x.Pulished,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
            }).ToList();
            return brandVMs;

        }

    }
}