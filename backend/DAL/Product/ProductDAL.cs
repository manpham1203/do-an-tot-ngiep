
using BO;
using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using BO.ViewModels.Product;
using BO.ViewModels.ProductImage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public class ProductDAL
    {
        private AppDbContext db;
        public ProductDAL()
        {
            db = new AppDbContext();
        }
        public async Task<List<ProductVM>> GetAll()
        {

            var productFromDb = await db.Products.ToListAsync();
            if (productFromDb.Count == 0)
            {
                return new List<ProductVM>();
            }
            var productVMs = productFromDb.Select(x => new ProductVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Price = x.Price,
                PriceDiscount = x.PriceDiscount,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Quantity = x.Quantity,
                Published = x.Published,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;
        }
        public async Task<ProductVM> GetById(string id)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (productFromDb == null)
            {
                return null;
            }
            var productVMs = new ProductVM
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Slug = productFromDb.Slug,
                Price = productFromDb.Price,
                PriceDiscount = productFromDb.PriceDiscount,
                FullDescription = productFromDb.FullDescription,
                ShortDescription = productFromDb.ShortDescription,
                Quantity = productFromDb.Quantity,
                Published = productFromDb.Published,
                Deleted = productFromDb.Deleted,
                Likes = productFromDb.Likes,
                Views = productFromDb.Views,
                CreatedAt = productFromDb.CreatedAt,
                UpdatedAt = productFromDb.UpdatedAt,
                BrandId = productFromDb.BrandId
            };
            return productVMs;

        }
        public async Task<List<ProductVM>> GetByBrandId(string id)
        {

            var productFromDb = await db.Products.Where(x => x.BrandId == id).ToListAsync();
            if (productFromDb == null)
            {
                return new List<ProductVM>();
            }
            var productVMs = productFromDb.Select(x => new ProductVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Price = x.Price,
                PriceDiscount = x.PriceDiscount,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Quantity = x.Quantity,
                Published = x.Published,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;
        }
        public async Task<bool> Create(ProductVM productVM)
        {

            var product = new BO.Entities.Product
            {
                Id = productVM.Id,
                Name = productVM.Name,
                Slug = productVM.Slug,
                Price = productVM.Price,
                PriceDiscount = productVM.PriceDiscount,
                FullDescription = productVM.FullDescription,
                ShortDescription = productVM.ShortDescription,
                Quantity = productVM.Quantity,
                Published = productVM.Published,
                Deleted = productVM.Deleted,
                Likes = productVM.Likes,
                Views = productVM.Views,
                CreatedAt = productVM.CreatedAt,
                UpdatedAt = productVM.UpdatedAt,
                BrandId = productVM.BrandId
            };
            await db.Products.AddAsync(product);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> Update(ProductVM productVM)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == productVM.Id);

            productFromDb.Name = productVM.Name;
            productFromDb.Slug = productVM.Slug;
            productFromDb.Price = productVM.Price;
            productFromDb.PriceDiscount = productVM.PriceDiscount;
            productFromDb.FullDescription = productVM.FullDescription;
            productFromDb.ShortDescription = productVM.ShortDescription;
            productFromDb.Quantity = productVM.Quantity;
            productFromDb.Published = productVM.Published;
            productFromDb.Deleted = productVM.Deleted;
            productFromDb.Likes = productVM.Likes;
            productFromDb.Views = productVM.Views;
            productFromDb.UpdatedAt = productVM.UpdatedAt;
            productFromDb.BrandId = productVM.BrandId;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }
        public async Task<bool> Delete(string id)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
            db.Products.Remove(productFromDb);
            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;

        }

        public async Task<bool> Pulished(string id, bool published)
        {
            var product = await db.Products.SingleOrDefaultAsync(x => x.Id == id);

            product.Published = published;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Deleted(string id, bool deleted)
        {
            var product = await db.Products.SingleOrDefaultAsync(x => x.Id == id);

            product.Deleted = deleted;

            var result = await db.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<List<ProductVM>> GetAllProductDeleted(bool deleted)
        {
            var products = await db.Products.Where(x => x.Deleted == deleted).ToListAsync();

            if (products.Count == 0)
            {
                return new List<ProductVM>();
            }

            var productVMs = products.Select(x => new ProductVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Price = x.Price,
                PriceDiscount = x.PriceDiscount,
                FullDescription = x.FullDescription,
                ShortDescription = x.ShortDescription,
                Quantity = x.Quantity,
                Published = x.Published,
                Deleted = x.Deleted,
                Likes = x.Likes,
                Views = x.Views,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;

        }
        public async Task<List<ProductCardVM>> ListProductCard()
        {
            try
            {
                var resultFromDb = await db.Products.Where(x => x.Published == true && x.Deleted == false).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductCardVM>();
                }
                var productCards = resultFromDb.Select(x => new ProductCardVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Price = x.Price,
                    PriceDiscount = x.PriceDiscount,
                    BrandNameVM = new BrandNameVM(),
                    ImageName = null,
                    ImageSrc = null,
                    BrandId = x.BrandId,
                }).ToList();
                return productCards;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProductCardVM> ProductCardById(string id)
        {
            try
            {
                var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id && x.Published == true && x.Deleted == false);
                if (resultFromDb == null)
                {
                    return null;
                }
                var productCard = new ProductCardVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    Price = resultFromDb.Price,
                    PriceDiscount = resultFromDb.PriceDiscount,
                    BrandNameVM = new BrandNameVM(),
                    ImageName = null,
                    ImageSrc = null,
                    BrandId = resultFromDb.BrandId,
                };
                return productCard;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ProductCardVM>> ListProductCardOfBrand(string id)
        {
            try
            {
                var resultFromDb = await db.Products.Where(x => x.Published == true && x.Deleted == false && x.BrandId == id).ToListAsync();
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductCardVM>();
                }
                if (resultFromDb == null)
                {
                    return null;
                }
                var productCards = resultFromDb.Select(x => new ProductCardVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    Price = x.Price,
                    PriceDiscount = x.PriceDiscount,
                    BrandNameVM = new BrandNameVM(),
                    ImageName = null,
                    ImageSrc = null,
                    BrandId = x.BrandId,
                }).ToList();
                return productCards;
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProductRowAdminVM> ProductRowAdmin(string id)
        {
            try
            {
                var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new ProductRowAdminVM();
                result.Id = resultFromDb.Id;
                result.Name = resultFromDb.Name;
                result.Slug = resultFromDb.Slug;
                result.Price = resultFromDb.Price;
                result.PriceDiscount = resultFromDb.PriceDiscount;
                result.Published = resultFromDb.Published;
                result.Deleted = resultFromDb.Deleted;
                result.Views = resultFromDb.Views;
                result.Likes = resultFromDb.Likes;
                result.CreatedAt = resultFromDb.CreatedAt;
                result.BrandId = resultFromDb.BrandId;
                result.BrandNameVM = new BrandNameVM();
                result.CategoryNameVMs = new List<CategoryNameVM>();
                result.ImageName = null;
                result.ImageSrc = null;
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProductNameVM>> AllProductName()
        {
            try
            {
                var resultFromDb = await db.Products.Where(x => x.Published == true && x.Deleted == false).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductNameVM>();
                }
                var result = resultFromDb.Select(x => new ProductNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    BrandId = x.BrandId,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<List<ProductNameVM>> AllProductName(bool deleted)
        {
            try
            {
                var resultFromDb = await db.Products.Where(x => x.Deleted == deleted).ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductNameVM>();
                }
                var result = resultFromDb.Select(x => new ProductNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    BrandId = x.BrandId,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<ProductNameVM>> AllProductNameAdmin(bool deleted, ProductFilterVM model)
        {
            try
            {
                var resultFromDb = await db.Products.Where(x => x.Deleted == deleted).OrderBy(x => x.CreatedAt).ToListAsync();

                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductNameVM>();
                }

                if (!string.IsNullOrEmpty(model.Search))
                {
                    resultFromDb = resultFromDb.Where(x => x.Name.ToLower().Contains(model.Search)).ToList();
                }
                if (model.From.HasValue)
                {
                    resultFromDb = resultFromDb.Where(x => x.Price >= model.From).ToList();
                }
                if (model.To.HasValue)
                {
                    resultFromDb = resultFromDb.Where(x => x.Price <= model.To).ToList();
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
                        case "price_asc":
                            resultFromDb = resultFromDb.OrderBy(x => x.Price).ToList();
                            break;
                        case "price_desc":
                            resultFromDb = resultFromDb.OrderByDescending(x => x.Price).ToList();
                            break;
                        default: break;
                    }
                }


                var result = resultFromDb.Select(x => new ProductNameVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Slug = x.Slug,
                    BrandId = x.BrandId,
                }).ToList();
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<ProductDetailVM> ProductDetail(string slug)
        {
            try
            {
                var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Slug == slug);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new ProductDetailVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    Price = resultFromDb.Price,
                    PriceDiscount = resultFromDb.PriceDiscount,
                    FullDescription = resultFromDb.FullDescription,
                    ShortDescription = resultFromDb.ShortDescription,
                    Quantity = resultFromDb.Quantity,
                    Published = resultFromDb.Published,
                    Deleted = resultFromDb.Deleted,
                    Views = resultFromDb.Views,
                    Likes = resultFromDb.Likes,
                    BrandId = resultFromDb.BrandId,
                    BrandNameVM = new BrandNameVM(),
                    CategoryNameVMs = new List<CategoryNameVM>(),
                    ProductImageVMs = new List<ProductImageVM>(),
                };
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<CartRowVM> CartRow(string id)
        {
            var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id && x.Published == true && x.Deleted == false);
            if (resultFromDb == null)
            {
                return null;
            }
            var result = new CartRowVM
            {
                Id = resultFromDb.Id,
                Name = resultFromDb.Name,
                Slug = resultFromDb.Slug,
                Price = resultFromDb.Price,
                PriceDiscount = resultFromDb.PriceDiscount,
                BrandId = resultFromDb.BrandId,
                BrandNameVM = null,
                ImageName = null,
                ImageSrc = null,
            };
            return result;
        }

        public async Task<bool> CheckExists(string id)
        {
            var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (resultFromDb == null)
            {
                return false;
            }
            return true;
        }

        public async Task<PriceRangeVM> PriceRange()
        {
            try
            {
                var maxPrice = await db.Products.MaxAsync(p => p.Price);
                var minPrice = await db.Products.MinAsync(p => p.Price);
                var result = new PriceRangeVM
                {
                    MaxPrice = maxPrice,
                    MinPrice = minPrice,
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
