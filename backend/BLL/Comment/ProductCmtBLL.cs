﻿using BLL.Category;
using BLL.Picture;
using BLL.ProductCategory;
using BLL.Wishlist;
using BO.ViewModels.Comment;
using BO.ViewModels.Product;
using DAL.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Comment
{
    public class ProductCmtBLL
    {
        private ProductCmtDAL productCmtDAL;
        private CommonBLL cm;
        public ProductCmtBLL()
        {
            productCmtDAL = new ProductCmtDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await productCmtDAL.CheckExists(id);
            }
            catch
            {
                return true;
            }
        }
        public async Task<bool> Create(ProductCmtVM productCmtVM)
        {
            try
            {
                cm = new CommonBLL();
                var cmtId = cm.RandomString(12);
                var checkExists = await CheckExists(cmtId);
                if (checkExists)
                {
                    cmtId = cm.RandomString(12);
                    checkExists = await CheckExists(cmtId);
                }
                productCmtVM.Content = string.IsNullOrEmpty(productCmtVM.Content) ? null : productCmtVM.Content;
                productCmtVM.Id = cmtId;
                productCmtVM.ObjectType = "product";
                productCmtVM.CreatedAt = DateTime.Now;
                productCmtVM.ParentId = null;
                productCmtVM.Published = true;
                return await productCmtDAL.Create(productCmtVM);
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductCmtVM> CommentItem(string id)
        {
            try
            {
                var resultFromDAL = await productCmtDAL.CommentItem(id);
                var children = await CmtChildren(id);
                resultFromDAL.Children = children;
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CmtPagination> IdsOfProduct(string productId, int limit, int currentPage)
        {
            try
            {
                var resultFromDAL = await productCmtDAL.IdsOfProduct(productId);
                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();

                return new CmtPagination
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    List = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<int?>> Star(string id)
        {
            try
            {
                return await productCmtDAL.Star(id);
            }
            catch
            {
                return null;
            }
        }


        public async Task<bool> RepCmt(string parentId, string content, string userId)
        {
            try
            {
                cm = new CommonBLL();
                var cmtId = cm.RandomString(12);
                var checkExists = await CheckExists(cmtId);
                var objectId = await CommentItem(parentId);
                if (checkExists)
                {
                    cmtId = cm.RandomString(12);
                    checkExists = await CheckExists(cmtId);
                }
                var cmtVM = new ProductCmtVM();
                cmtVM.Content = content;
                cmtVM.Id = cmtId;
                cmtVM.ObjectType = "product";
                cmtVM.CreatedAt = DateTime.Now;
                cmtVM.ParentId = parentId;
                cmtVM.ObjectId = objectId.ObjectId;
                cmtVM.UserId = userId;
                cmtVM.Published = true;
                return await productCmtDAL.Create(cmtVM);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ProductCmtVM>> CmtChildren(string parentId)
        {
            try
            {
                return await productCmtDAL.CmtChildren(parentId);
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<CmtPagination2> CmtPagination(int? star, string objectType, int currentPage, int limit)
        {
            try
            {
                var resultFromDAL = await productCmtDAL.GetListCmt(objectType);
                if (star.HasValue)
                {
                    var newList = new List<CmtRowAminVM>();
                    for (int i = 0; i < resultFromDAL.Count; i++)
                    {
                        var checkStar = await GetStarByCmtId(resultFromDAL[i].Id);
                        if (checkStar == star)
                        {
                            newList.Add(resultFromDAL[i]);
                        }
                    }
                    var newCount = newList.Count();
                    var newTotalPage = (int)Math.Ceiling(newCount / (double)limit);
                    newList = newList.Skip((currentPage - 1) * limit).Take(limit).ToList();

                    return new CmtPagination2
                    {
                        TotalResult = newCount,
                        TotalPage = newTotalPage,
                        List = newList,
                    };
                }


                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();

                return new CmtPagination2
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    List = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<int?> GetStarByCmtId(string id)
        {
            try
            {
                return await productCmtDAL.GetStarByCmtId(id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> GetOrderDetailId(string cmtId)
        {
            try
            {
                return await productCmtDAL.GetOrderDetailId(cmtId);
            }
            catch
            {
                return null;
            }
        }
        public async Task<string> GetProductId(string id)
        {
            try
            {
                return await productCmtDAL.GetProductId(id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<ProductDetailVM> ProductDetailAdmin(string cmtId)
        {
            var orderDetaiId = await GetOrderDetailId(cmtId);
            var productId = await GetProductId(orderDetaiId);
            var resultFromDAL = await productCmtDAL.ProductDetailAdmin(productId);
            if (resultFromDAL == null)
            {
                return null;
            }
            var brandBLL = new BrandBLL();
            var brand = await brandBLL.GetById(resultFromDAL.BrandId);
            if (brand != null)
            {
                resultFromDAL.BrandNameVM.Id = brand.Id;
                resultFromDAL.BrandNameVM.Name = brand.Name;
                resultFromDAL.BrandNameVM.Slug = brand.Slug;
            }

            var pcBLL = new ProductCategoryBLL();
            var listCategoryProduct = await pcBLL.GetById(resultFromDAL.Id, "ProductId");
            if (listCategoryProduct.Count > 0)
            {
                for (int j = 0; j < listCategoryProduct.Count(); j++)
                {
                    var categoryBLL = new CategoryBLL();
                    var category = await categoryBLL.CategoryNameById(listCategoryProduct[j].CategoryId);
                    if (category == null)
                    {
                        continue;
                    }
                    resultFromDAL.CategoryNameVMs.Add(category);
                }
            }

            var productImageBLL = new PictureBLL();
            var listImg = await productImageBLL.GetByObjectId(resultFromDAL.Id, "product");
            resultFromDAL.PictureVMs = listImg;

            var cmtBLL = new ProductCmtBLL();

            var star = await cmtBLL.Star(resultFromDAL.Id);

            if (star.Count() == 0)
            {
                resultFromDAL.Star = 0;
                resultFromDAL.StarCount = 0;
            }
            else
            {
                resultFromDAL.Star = star.Sum(x => x.Value) / (float)star.Count();
                resultFromDAL.StarCount = star.Count();
            }

            var wishlistBLL = new WishlistBLL();
            var wishlist = await wishlistBLL.Count(resultFromDAL.Id);

            resultFromDAL.Like = wishlist;

            return resultFromDAL;
        }
    }
}
