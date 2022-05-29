using BO;
using BO.ViewModels.Brand;
using BO.ViewModels.Picture;
using BO.ViewModels.Product;
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
        //public async Task<List<BrandVM>> GetAll()
        //{
        //    var brandFromDb = await db.Brands.ToListAsync();

        //    if (brandFromDb.Count == 0)
        //    {
        //        return new List<BrandVM>();
        //    }

        //    var brandVMs = brandFromDb.Select(x => new BrandVM
        //    {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Slug = x.Slug,
        //        FullDescription = x.FullDescription,
        //        ShortDescription = x.ShortDescription,
        //        Published = x.Published,
        //        Deleted = x.Deleted,
        //        CreatedAt = x.CreatedAt,
        //        UpdatedAt = x.UpdatedAt,
        //        Ordinal = x.Ordinal,
        //    }).ToList();
        //    return brandVMs;

        //}
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
            brandVM.Published = brandFromDb.Published;
            brandVM.Deleted = brandFromDb.Deleted;
            brandVM.CreatedAt = brandFromDb.CreatedAt;
            brandVM.UpdatedAt = brandFromDb.UpdatedAt;

            return brandVM;
        }
        //public async Task<BrandVM> GetBySlug(string slug)
        //{
        //    //var resultFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Slug == slug);
        //    var resultFromDb = await (from b in db.Brands
        //                       join p in db.Pictures on b.Id equals p.ObjectId
        //                              where b.Slug == slug
        //                       select new BrandVM
        //                       {
        //                           Id = b.Id,
        //                           Name = p.Name,
        //                           Slug = b.Slug,
        //                           FullDescription = b.FullDescription,
        //                           ShortDescription=b.ShortDescription,
        //                           Published = b.Published,
        //                           Deleted=b.Deleted,
        //                           CreatedAt=b.CreatedAt,
        //                           UpdatedAt=b.UpdatedAt,
        //                           Ordinal=b.Ordinal,
        //                           Image=p.Name,
        //                           ImageSrc=null,
        //                       }).SingleOrDefaultAsync();
        //    if (resultFromDb == null)
        //    {
        //        return null;
        //    }
        //    var brandVM = new BrandVM();

        //    brandVM.Id = resultFromDb.Id;
        //    brandVM.Name = resultFromDb.Name;
        //    brandVM.Slug = resultFromDb.Slug;
        //    brandVM.FullDescription = resultFromDb.FullDescription;
        //    brandVM.ShortDescription = resultFromDb.ShortDescription;
        //    brandVM.Published = resultFromDb.Published;
        //    brandVM.Deleted = resultFromDb.Deleted;
        //    brandVM.CreatedAt = resultFromDb.CreatedAt;
        //    brandVM.UpdatedAt = resultFromDb.UpdatedAt;
        //    brandVM.Ordinal = resultFromDb.Ordinal;

        //    return brandVM;
        //}
        public async Task<bool> CheckExistsId(string id)
        {
            try
            {
                var resultFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
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
        public async Task<bool> CheckExistsSlug(string slug)
        {
            try
            {
                var resultFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Slug == slug);
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
        public async Task<bool> Create(BrandVM brandVM, PictureVM pictureVM)
        {
            try
            {
                var brand = new BO.Entities.Brand
                {
                    Id = brandVM.Id,
                    Name = brandVM.Name,
                    Slug = brandVM.Slug,
                    Published = brandVM.Published,
                    Deleted = brandVM.Deleted,
                    CreatedAt = brandVM.CreatedAt,
                    UpdatedAt = brandVM.UpdatedAt,
                };
                await db.Brands.AddAsync(brand);
                var resultBrand = await db.SaveChangesAsync();
                if (resultBrand == 0)
                {
                    return false;
                }

                var picture = new BO.Entities.Picture
                {
                    Id = pictureVM.Id,
                    Name = pictureVM.Name,
                    Published = pictureVM.Published,
                    ObjectId = pictureVM.ObjectId,
                    ObjectType = pictureVM.ObjectType,
                };

                await db.Pictures.AddAsync(picture);
                var resultPicture = await db.SaveChangesAsync();
                if (resultPicture == 0)
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
        public async Task<bool> Update(BrandVM brandVM, PictureVM pictureVM)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == brandVM.Id);
            brandFromDb.Name = brandVM.Name;
            brandFromDb.Slug = brandVM.Slug;
            brandFromDb.Published = brandVM.Published;
            brandFromDb.Deleted = brandVM.Deleted;
            brandFromDb.UpdatedAt = brandVM.UpdatedAt;

            var resultBrand = await db.SaveChangesAsync();
            if (resultBrand == 0)
            {
                return false;
            }
            if (pictureVM.Name != null)
            {
                var pictureFromDb = await db.Pictures.SingleOrDefaultAsync(x => x.ObjectId == brandFromDb.Id);
                pictureFromDb.Name = pictureVM.Name;

                var resultPicture = await db.SaveChangesAsync();
                if (resultPicture == 0)
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<bool> Delete(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
            db.Brands.Remove(brandFromDb);
            var resultBrand = await db.SaveChangesAsync();
            if (resultBrand == 0)
            {
                return false;
            }
            var pictureFromDb = await db.Pictures.Where(x=>x.ObjectId==id).Where(x=>x.ObjectType=="brand").SingleOrDefaultAsync();
            db.Pictures.Remove(pictureFromDb);
            var resultPicture = await db.SaveChangesAsync();
            if(resultPicture == 0)
            {
                return false;
            }


            return true;
        }
        public async Task<bool> Pulished(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);

            brandFromDb.Published = !brandFromDb.Published;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id)
        {
            var brandFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);

            brandFromDb.Deleted = !brandFromDb.Deleted;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<BrandVM>> GetAllBrandDeleted(bool deleted)
        {
            var brandFromDb = await db.Brands.Where(x => x.Deleted == deleted).ToListAsync();

            if (brandFromDb.Count == 0)
            {
                return new List<BrandVM>();
            }

            var brandVMs = brandFromDb.Select(x => new BrandVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Published = x.Published,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
            }).ToList();
            return brandVMs;

        }

        public async Task<List<BrandNameVM>> AllBrandName()
        {
            try
            {
                var resultFromDb = await db.Brands.Where(x => x.Published == true && x.Deleted == false).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<BrandNameVM>();
                }
                var listBrandNameVM = resultFromDb.Select(x => new BrandNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();
                return listBrandNameVM;
            }
            catch
            {
                return null;
            }

        }

        public async Task<List<BrandNameVM>> AllBrandName(bool deleted)
        {
            try
            {
                var resultFromDb = await db.Brands.Where(x => x.Deleted == deleted).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<BrandNameVM>();
                }
                var listBrandNameVM = resultFromDb.Select(x => new BrandNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();
                return listBrandNameVM;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BrandNameVM> BrandWithProductCard(string id)
        {
            try
            {
                var resultFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new BrandNameVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    ProductCardVMs = null,
                };
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BrandNameVM>> AllBrandNameAdmin(bool deleted, BrandFilterVM model)
        {
            try
            {
                var resultFromDb = await db.Brands.Where(x => x.Deleted == deleted).OrderBy(x => x.CreatedAt).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<BrandNameVM>();
                }

                if (!string.IsNullOrEmpty(model.Search))
                {
                    resultFromDb = resultFromDb.Where(x => x.Name.ToLower().Contains(model.Search)).ToList();
                }

                if (!string.IsNullOrEmpty(model.ShortBy))
                {
                    switch (model.ShortBy)
                    {
                        case "desc":
                            resultFromDb = resultFromDb.OrderByDescending(x => x.Name).ToList();
                            break;
                        case "asc":
                            resultFromDb = resultFromDb.OrderBy(x => x.Name).ToList();
                            break;
                        default: break;
                    }
                }

                var listBrandNameVM = resultFromDb.Select(x => new BrandNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();

                return listBrandNameVM;
            }
            catch
            {
                return null;
            }
        }

        public async Task<BrandRowAdminVM> BrandRowAmin(string id)
        {
            try
            {
                var resultFromDb = await db.Brands.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new BrandRowAdminVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    Published = resultFromDb.Published,
                    Deleted = resultFromDb.Deleted,
                    CreatedAt = resultFromDb.CreatedAt,
                };
                return result;
            }
            catch
            {
                return null;
            }

        }
        public async Task<BrandVM> BrandDetail(string id)
        {
            try
            {
                var resultFromDb = await (from b in db.Brands
                                          join p in db.Pictures on b.Id equals p.ObjectId
                                          where b.Id == id && p.ObjectType=="brand"
                                          select new BrandVM
                                          {
                                              Id=b.Id,
                                              Name=b.Name,
                                              Slug=b.Slug,
                                              Published=b.Published,
                                              Deleted=b.Deleted,
                                              CreatedAt=b.CreatedAt,
                                              UpdatedAt=b.UpdatedAt,
                                              Image=p.Name,
                                              ImageSrc=null,
                                          }).SingleOrDefaultAsync();
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }
    
        public async Task<List<BrandChartVM>> BrandChart()
        {
            try
            {
                //var resultFromDb = await (from b in db.Brands join p in db.Products on b.Id equals p.BrandId
                //                          where b.Deleted==false
                //                          select new BrandChartVM
                //                          {
                //                              Id=b.Id,
                //                              Name=b.Name,
                //                              ProductQty=p.
                //                          }
                //                          ).ToListAsync();
                var resultFromDb = await db.Brands.Where(x => x.Deleted == false).ToListAsync();

                var result = resultFromDb.Where(x => x.Deleted == false)
                    .Join(db.Products, b=>b.Id, p=>p.BrandId,(b,p)=>new {b,p})
                    .GroupBy(x=>x.b.Id).Select(x=>new BrandChartVM
                    {
                        Id=x.Key,
                        Name=x.First().b.Name,
                        ProductQty=x.Count()
                    }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}
