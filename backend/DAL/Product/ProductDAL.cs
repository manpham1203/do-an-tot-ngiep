
using BO;
using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using BO.ViewModels.Picture;
using BO.ViewModels.Product;
using BO.ViewModels.ProductCategory;
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
                QuantityInStock = x.QuantityInStock,
                Published = x.Published,
                Deleted = x.Deleted,
                View = x.View,
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
                QuantityInStock = productFromDb.QuantityInStock,
                Published = productFromDb.Published,
                Deleted = productFromDb.Deleted,
                View = productFromDb.View,
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
                QuantityInStock = x.QuantityInStock,
                Published = x.Published,
                Deleted = x.Deleted,
                View = x.View,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                BrandId = x.BrandId
            }).ToList();
            return productVMs;
        }
        public async Task<bool> Create(ProductVM productVM, List<PictureVM> pictureVMs, List<ProductCategoryVM> pcVMs)
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
                QuantityInStock = productVM.QuantityInStock,
                Published = productVM.Published,
                Deleted = productVM.Deleted,
                View = productVM.View,
                CreatedAt = productVM.CreatedAt,
                UpdatedAt = productVM.UpdatedAt,
                BrandId = productVM.BrandId
            };
            await db.Products.AddAsync(product);
            var resultProduct = await db.SaveChangesAsync();

            if (resultProduct == 0)
            {
                return false;
            }

            var productCategoryVM = pcVMs.Select(x => new BO.Entities.ProductCategory
            {
                CategoryId = x.CategoryId,
                ProductId = x.ProductId,
            });
            await db.Product_Category_Mappings.AddRangeAsync(productCategoryVM);
            var resultPC = await db.SaveChangesAsync();
            if (resultPC == 0)
            {
                return false;
            }

            var pictutes = pictureVMs.Select(x => new BO.Entities.Picture
            {
                Id = x.Id,
                Name = x.Name,
                ObjectId = x.ObjectId,
                ObjectType = x.ObjectType,
                Published = x.Published,
            }).ToList();

            await db.Pictures.AddRangeAsync(pictutes);
            var resultPicture = await db.SaveChangesAsync();
            if (resultPicture != pictutes.Count)
            {
                return false;
            }

            return true;

        }
        public async Task<bool> Update(ProductVM productVM, List<PictureVM> pictureVMs, List<ProductCategoryVM> pcVMs)
        {

            var productFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == productVM.Id);

            productFromDb.Name = productVM.Name;
            productFromDb.Slug = productVM.Slug;
            productFromDb.Price = productVM.Price;
            productFromDb.PriceDiscount = productVM.PriceDiscount;
            productFromDb.FullDescription = productVM.FullDescription;
            productFromDb.ShortDescription = productVM.ShortDescription;
            productFromDb.QuantityInStock = productVM.QuantityInStock;
            productFromDb.Published = productVM.Published;
            productFromDb.Deleted = productVM.Deleted;
            productFromDb.View = productVM.View;
            productFromDb.UpdatedAt = productVM.UpdatedAt;
            productFromDb.BrandId = productVM.BrandId;

            var resultProduct = await db.SaveChangesAsync();
            if (resultProduct == 0)
            {
                return false;
            }
            var productCategory = pcVMs.Select(x => new BO.Entities.ProductCategory
            {
                CategoryId = x.CategoryId,
                ProductId = x.ProductId,
            }).ToList();
            var listCurrentPC = await db.Product_Category_Mappings.Where(x => x.ProductId == productFromDb.Id).ToListAsync();
            db.Product_Category_Mappings.RemoveRange(listCurrentPC);
            var resultPCDelete = await db.SaveChangesAsync();
            if (resultPCDelete != listCurrentPC.Count)
            {
                return false;
            }

            await db.Product_Category_Mappings.AddRangeAsync(productCategory);
            var resultPC = await db.SaveChangesAsync();
            if (resultPC != pcVMs.Count)
            {
                return false;
            }


            var pictutes = pictureVMs.Select(x => new BO.Entities.Picture
            {
                Id = x.Id,
                Name = x.Name,
                ObjectId = x.ObjectId,
                ObjectType = x.ObjectType,
                Published = x.Published,
            }).ToList();

            await db.Pictures.AddRangeAsync(pictutes);
            var resultPicture = await db.SaveChangesAsync();
            if (resultPicture != pictutes.Count)
            {
                return false;
            }

            return true;

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
                QuantityInStock = x.QuantityInStock,
                Published = x.Published,
                Deleted = x.Deleted,
                View = x.View,
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
                var resultFromDb = await db.Products.Where(x => x.Published == true && x.Deleted == false).OrderByDescending(x=>x.CreatedAt).ToListAsync();
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
        public async Task<ProductCardVM> ProductCard(string id)
        {
            try
            {
                var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id && x.Published == true && x.Deleted == false);

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
                result.View = resultFromDb.View;
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
                if (model.PriceFrom.HasValue)
                {
                    resultFromDb = resultFromDb.Where(x => x.Price >= model.PriceFrom).ToList();
                }
                if (model.PriceTo.HasValue)
                {
                    resultFromDb = resultFromDb.Where(x => x.Price <= model.PriceTo).ToList();
                }


                if (!string.IsNullOrEmpty(model.OrderBy))
                {
                    switch (model.OrderBy)
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
                    QuantityInStock = resultFromDb.QuantityInStock,
                    Published = resultFromDb.Published,
                    Deleted = resultFromDb.Deleted,
                    View = resultFromDb.View,
                    BrandId = resultFromDb.BrandId,
                    BrandNameVM = new BrandNameVM(),
                    CategoryNameVMs = new List<CategoryNameVM>(),
                    PictureVMs = new List<PictureVM>(),
                    Star = null,
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
                CurrentPrice = resultFromDb.PriceDiscount != null ? resultFromDb.PriceDiscount : resultFromDb.Price,
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

        public async Task<List<ProductWidgetVM>> NewProductWidget()
        {
            var resultFromDb = await db.Products.Where(x => x.Published == true && x.Deleted == false).OrderBy(x => x.CreatedAt).Take(5).ToListAsync();
            if (resultFromDb.Count == 0)
            {
                return new List<ProductWidgetVM>();
            }
            if (resultFromDb == null)
            {
                return null;
            }
            var result = resultFromDb.Select(x => new ProductWidgetVM
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Price = x.Price,
                PriceDiscount = x.PriceDiscount,
                ImgName = null,
                ImgSrc = null,
            }).ToList();
            return result;
        }

        public async Task<List<ProductCardVM>> ProductFilter(ProductFilterVM model)
        {
            try
            {
                var resultFromDb = await db.Products.Where(x => x.Deleted == false && x.Published == true).ToListAsync();

                if (resultFromDb == null)
                {
                    return null;
                }
                if (resultFromDb.Count == 0)
                {
                    return new List<ProductCardVM>();
                }

                if (!string.IsNullOrEmpty(model.Search))
                {
                    resultFromDb = resultFromDb.Where(x => x.Name.ToLower().Contains(model.Search)).ToList();
                }
                if (model.Discount != null)
                {
                    if (model.Discount == false)
                    {
                        resultFromDb = resultFromDb.Where(x => x.PriceDiscount == null).ToList();
                    }
                    else
                    {
                        resultFromDb = resultFromDb.Where(x => x.PriceDiscount != null).ToList();
                    }
                }
                if (!string.IsNullOrEmpty(model.PriceRange))
                {
                    switch (model.PriceRange)
                    {
                        case "duoi-5-trieu":
                            resultFromDb = resultFromDb.Where(x => x.Price < 5000000).ToList();
                            break;
                        case "tu-5-10-trieu":
                            resultFromDb = resultFromDb.Where(x => x.Price >= 5000000 && x.Price <= 10000000).ToList();
                            break;
                        case "tu-10-15-trieu":
                            resultFromDb = resultFromDb.Where(x => x.Price >= 10000000 && x.Price <= 15000000).ToList();
                            break;
                        case "tu-15-20-trieu":
                            resultFromDb = resultFromDb.Where(x => x.Price >= 15000000 && x.Price <= 20000000).ToList();
                            break;
                        case "tren-20":
                            resultFromDb = resultFromDb.Where(x => x.Price > 20000000).ToList();
                            break;
                        default: break;
                    }
                }

                if (!string.IsNullOrEmpty(model.OrderBy))
                {
                    switch (model.OrderBy)
                    {
                        case "day_desc":
                            resultFromDb = resultFromDb.OrderByDescending(x => x.CreatedAt).ToList();
                            break;
                        case "day_asc":
                            resultFromDb = resultFromDb.OrderBy(x => x.CreatedAt).ToList();
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


                var result = resultFromDb.Select(x => new ProductCardVM
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
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ProductOrderVM> GetProductByOrderDetail(string id)
        {
            try
            {
                var resultFromDb = await db.Products.Select(x => new { x.Id, x.Name, x.Slug, x.BrandId }).SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = new ProductOrderVM
                {
                    Id = resultFromDb.Id,
                    Name = resultFromDb.Name,
                    Slug = resultFromDb.Slug,
                    BrandId = resultFromDb.BrandId,
                    ImageName = null,
                    ImageSrc = null,
                    BrandNameVM = null,
                };
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IncreaseView(string id)
        {
            try
            {
                var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == id);
                if (resultFromDb == null)
                {
                    return false;
                }
                resultFromDb.View += 1;
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

        public async Task<List<ProductCardVM>> ProductWishlist(string userId)
        {
            try
            {
                var resultFromDb = await (from w in db.Wishlists
                                          join p in db.Products on w.ProductId equals p.Id into list
                                          from p in list.DefaultIfEmpty()
                                          where w.UserId == userId && p.Published == true && p.Deleted == false
                                          select new ProductCardVM
                                          {
                                              Id = p.Id,
                                              Name = p.Name,
                                              Slug = p.Slug,
                                              Price = p.Price,
                                              PriceDiscount = p.PriceDiscount,
                                              BrandNameVM = new BrandNameVM(),
                                              ImageName = null,
                                              ImageSrc = null,
                                              BrandId = p.BrandId,
                                          }).ToListAsync();
                return resultFromDb;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> PublishedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Published = true;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> PublishedFalseList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Published = false;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Deleted = true;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeletedFalseList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var resultFromDb = await db.Products.SingleOrDefaultAsync(x => x.Id == ids[i]);
                    resultFromDb.Deleted = false;
                }
                var result = await db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductCardVM>> MostBought()
        {
            try
            {
                var resultFromDb = await db.OrderDetails.ToListAsync();
                if (resultFromDb == null)
                {
                    return null;
                }
                var result = resultFromDb.Join(db.Products, d => d.ProductId, p => p.Id, (d, p) => new { d, p })
                    .GroupBy(x => x.d.ProductId)
                    .Where(x => x.Sum(m => m.d.Quantity) > 10 && x.First().p.Deleted==false && x.First().p.Published == true)
                    .Select(x => new ProductCardVM
                    {
                        Id = x.Key,
                        Name = x.First().p.Name,
                        Slug = x.First().p.Slug,
                        Price = x.First().p.Price,
                        PriceDiscount = x.First().p.PriceDiscount,
                        BrandNameVM = new BrandNameVM(),
                        ImageName = null,
                        ImageSrc = null,
                        BrandId = x.First().p.BrandId,
                    }).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ProductCardVM>> OnSale(int take)
        {
            try
            {
                var resultFromDb = await db.Products.Take(take).Where(x => x.PriceDiscount !=null && x.Published == true && x.Deleted == false).OrderBy(x => x.PriceDiscount).ToListAsync();
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
    }
}
