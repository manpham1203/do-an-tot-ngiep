using BO.ViewModels.Comment;
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
            productCmtDAL=new ProductCmtDAL();
        }
        public async Task<List<ProductCmtVM>> ProductCmtVMs(string produtctId)
        {
            try
            {
                return await productCmtDAL.ProductCmts(produtctId);
            }
            catch
            {
                return null;
            }
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
                productCmtVM.Id = cmtId;
                productCmtVM.ObjectType = "product";
                productCmtVM.CreatedAt = DateTime.Now;
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
                return await productCmtDAL.CommentItem(id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<string>> IdsOfProduct(string productId)
        {
            try
            {
                return await productCmtDAL.IdsOfProduct(productId);
            }
            catch
            {
                return null;
            }
        }
    }
}
