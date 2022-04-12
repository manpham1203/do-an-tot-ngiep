using BO.ViewModels.ProductImage;
using DAL.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ProductImage
{
    public class ProductImageBLL
    {
        private ProductImageDAL productImageDAL;
        private CommonBLL cm;

        public ProductImageBLL()
        {
            productImageDAL = new ProductImageDAL();
        }
        public async Task<ProductImageVM> GetById(string id)
        {
            return await productImageDAL.GetById(id);
        }
        public async Task<List<ProductImageVM>> GetByProductId(string id)
        {
            return await productImageDAL.GetByProductId(id);
        }
        public async Task<bool> Create(List<string> imgName, string productId)
        {
            cm = new CommonBLL();
            List<ProductImageVM> productImageVMs = new List<ProductImageVM>();
            ProductImageVM productImageVM;
            for (int i = 0; i < imgName.Count; i++)
            {
                var imgId = cm.RandomString(12);
                var checkImg = await GetById(imgId);
                while (checkImg != null)
                {
                    imgId = cm.RandomString(12);
                    checkImg = await GetById(imgId);
                }
                productImageVM = new ProductImageVM
                {
                    Id = imgId,
                    ProductId = productId,
                    Name = imgName[i],
                    Published = true,
                };
                productImageVMs.Add(productImageVM);
            }
            return await productImageDAL.Create(productImageVMs);
        }
        public async Task<bool> Delete(string id)
        {
            var productImage = await GetById(id);
            if (productImage == null)
            {
                return false;
            }
            return await productImageDAL.Delete(id);
        }
        public async Task<bool> Published(string id)
        {
            var productImageVM = await GetById(id);
            if (productImageVM == null)
            {
                return false;
            }
            bool pulished = !productImageVM.Published;
            var result = await productImageDAL.Pulished(id, pulished);
            if (result)
            {
                return true;
            }
            return false;
        }

    }
}
