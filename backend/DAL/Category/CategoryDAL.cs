
using BO;
using BO.ViewModels.Category;
using BO.ViewModels.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Category
{
    public class CategoryDAL
    {
        private readonly AppDbContext db;
        public CategoryDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<CategoryVM>> GetAll()
        {
            var categoryFromDb = await db.Categories.ToListAsync();
            if (categoryFromDb.Count == 0)
            {
                return new List<CategoryVM>();
            }
            var categoryVMs = categoryFromDb.Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Published = x.Published,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
            }).ToList();
            return categoryVMs;
        }
        public async Task<CategoryVM> GetById(string id)
        {
            var categoryFromDb = await db.Categories.SingleOrDefaultAsync(c => c.Id == id);
            if (categoryFromDb == null)
            {
                return null;
            }
            var categoryVM = new CategoryVM();

            categoryVM.Id = categoryFromDb.Id;
            categoryVM.Name = categoryFromDb.Name;
            categoryVM.Slug = categoryFromDb.Slug;
            categoryVM.FullDescription = categoryFromDb.FullDescription;
            categoryVM.ShortDescription = categoryFromDb.ShortDescription;
            categoryVM.Published = categoryFromDb.Published;
            categoryVM.Deleted = categoryFromDb.Deleted;
            categoryVM.CreatedAt = categoryFromDb.CreatedAt;
            categoryVM.UpdatedAt = categoryFromDb.UpdatedAt;
            categoryVM.Ordinal = categoryFromDb.Ordinal;

            return categoryVM;
        }
        public async Task<bool> Create(CategoryVM categoryVM)
        {
            var category = new BO.Entities.Category
            {
                Id = categoryVM.Id,
                Name = categoryVM.Name,
                Slug = categoryVM.Slug,
                FullDescription = categoryVM.FullDescription,
                ShortDescription = categoryVM.ShortDescription,
                Published = categoryVM.Published,
                Deleted = categoryVM.Deleted,
                CreatedAt = categoryVM.CreatedAt,
                UpdatedAt = categoryVM.UpdatedAt,
            };
            await db.Categories.AddAsync(category);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(CategoryVM categoryVM)
        {
            var categoryFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == categoryVM.Id);

            categoryFromDb.Name = categoryVM.Name;
            categoryFromDb.Slug = categoryVM.Slug;
            categoryFromDb.FullDescription = categoryVM.FullDescription;
            categoryFromDb.ShortDescription = categoryVM.ShortDescription;
            categoryFromDb.Published = categoryVM.Published;
            categoryFromDb.Deleted = categoryVM.Deleted;
            categoryFromDb.UpdatedAt = categoryVM.UpdatedAt;
            categoryFromDb.Ordinal = categoryVM.Ordinal;
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string id)
        {
            var categoryFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id);
            db.Categories.Remove(categoryFromDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Pulished(string id, bool published)
        {
            var resultFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id);

            resultFromDb.Published = published;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id, bool deleted)
        {
            var resultFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id);

            resultFromDb.Deleted = deleted;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<CategoryVM>> GetAllCategoryDeleted(bool deleted)
        {
            var resultFromDb = await db.Categories.Where(x => x.Deleted == deleted).ToListAsync();

            if (resultFromDb.Count == 0)
            {
                return new List<CategoryVM>();
            }

            var categoryVMs = resultFromDb.Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Published = x.Published,
                Deleted = x.Deleted,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                Ordinal = x.Ordinal,
            }).ToList();
            return categoryVMs;

        }

        public async Task<List<CategoryNameVM>> AllCategoryNameAdmin(bool deleted, CategoryFilterVM model)
        {
            try
            {
                var resultFromDb = await db.Categories.Where(x => x.Deleted == deleted).OrderBy(x => x.CreatedAt).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<CategoryNameVM>();
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

                var categoryNameVMs = resultFromDb.Select(x => new CategoryNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();

                return categoryNameVMs;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<CategoryNameVM>> AllCategoryName()
        {
            try
            {
                var resultFromDb = await db.Categories.Where(x => x.Published == true && x.Deleted == false).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<CategoryNameVM>();
                }
                var categoryNameVMs = resultFromDb.Select(x => new CategoryNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();
                return categoryNameVMs;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<CategoryNameVM>> AllCategoryName(bool deleted)
        {
            try
            {
                var resultFromDb = await db.Categories.Where(x => x.Deleted == deleted).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<CategoryNameVM>();
                }
                var categoryNameVMs = resultFromDb.Select(x => new CategoryNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    ProductCardVMs = null,
                }).ToList();
                return categoryNameVMs;
            }
            catch
            {
                return null;
            }

        }

        public async Task<CategoryNameVM> CategoryNameById(string id)
        {
            try
            {
                var resultFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id && x.Published == true && x.Deleted == false);
                if (resultFromDb == null)
                {
                    return null;
                }
                var categoryNameVM =new CategoryNameVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    ProductCardVMs = null,
                };
                return categoryNameVM;
            }
            catch
            {
                return null;
            }

        }

        public async Task<CategoryRowAdminVM> CategoryRowAdmin(string id)
        {
            try
            {
                var resultFromDb = await db.Categories.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new CategoryRowAdminVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    Published = resultFromDb.Published,
                    Deleted = resultFromDb.Deleted,
                    CreatedAt = resultFromDb.CreatedAt,
                    Ordinal = resultFromDb.Ordinal,
                };
                return result;
            }
            catch
            {
                return null;
            }
        }
        

    }
}
