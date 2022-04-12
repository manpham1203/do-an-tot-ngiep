using BLL.BrandImage;
using BLL.Product;
using BO.ViewModels.Brand;
using BO.ViewModels.BrandImage;
using BO.ViewModels.Product;
using DAL.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Brand
{
    public class BrandFullBLL
    {
        private readonly BrandFullDAL brandFullDAL;
        public BrandFullBLL()
        {
            brandFullDAL = new BrandFullDAL();
        }
        public async Task<List<BrandFullVM>> GetAll()
        {
            var brandFullVMs = await brandFullDAL.GetAll();
            if (brandFullVMs.Count == 0)
            {
                return brandFullVMs;
            }
            //var productBLL = new ProductBLL();
            //for (int i = 0; i < brandFullVMs.Count; i++)
            //{
            //    var listProduct = await productBLL.GetByBrandId(brandFullVMs[i].Id);
            //    if (listProduct.Count>0)
            //    {
            //        brandFullVMs[i].ProductVMs = new List<ProductVM>();
            //        for (int j = 0; j < listProduct.Count; j++)
            //        {
            //            brandFullVMs[i].ProductVMs.Add(listProduct[j]);
            //        }
            //    }

            //}
            var productFullBLL = new ProductFullBLL();
            for (int i = 0; i < brandFullVMs.Count; i++)
            {
                var listProduct = await productFullBLL.GetByBrandId(brandFullVMs[i].Id);
                    //brandFullVMs[i].ProductFullVMs = new List<ProductFullVM>();
                if (listProduct.Count > 0)
                {
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        brandFullVMs[i].ProductFullVMs.Add(listProduct[j]);
                    }
                }
            }

            var brandImgBLL = new BrandImageBLL();
            for (int i = 0; i < brandFullVMs.Count; i++)
            {
                var listImg = await brandImgBLL.GetByBrandId(brandFullVMs[i].Id);
                if (listImg.Count > 0 )
                {
                    //brandFullVMs[i].BrandImageVMs = new List<BrandImageVM>();
                    for (int j = 0; j < listImg.Count; j++)
                    {
                        brandFullVMs[i].BrandImageVMs.Add(listImg[j]);
                    }
                }
            }


            return brandFullVMs;
        }
        public async Task<BrandFullVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var brandFullVM = await brandFullDAL.GetById(id);
            if (brandFullVM == null)
            {
                return null;
            }
            var productBLL = new ProductBLL();

            var listProduct = await productBLL.GetByBrandId(brandFullVM.Id);
            if (listProduct.Count == 0)
            {
                brandFullVM.ProductVMs = new List<ProductVM>();
            }
            else
            {
                brandFullVM.ProductVMs = new List<ProductVM>();
                for (int j = 0; j < listProduct.Count; j++)
                {
                    brandFullVM.ProductVMs.Add(listProduct[j]);
                }
            }

            var brandImgBLL = new BrandImageBLL();
            var listImg = await brandImgBLL.GetByBrandId(brandFullVM.Id);
            if (listImg.Count > 0)
            {
                brandFullVM.BrandImageVMs = new List<BrandImageVM>();
                for (int i = 0; i < listImg.Count; i++)
                {
                    brandFullVM.BrandImageVMs.Add(listImg[i]);
                }
            }

            return brandFullVM;
        }
        public async Task<BrandFullVM> GetBySlug(string slug)
        {
            var brandFullVM = await brandFullDAL.GetBySlug(slug);
            if (brandFullVM == null)
            {
                return null;
            }

            var productBLL = new ProductBLL();
            var listProduct = await productBLL.GetByBrandId(brandFullVM.Id);
            if (listProduct == null)
            {
                brandFullVM.ProductVMs = new List<ProductVM>();
            }
            else
            {
                brandFullVM.ProductVMs = new List<ProductVM>();
                for (int j = 0; j < listProduct.Count; j++)
                {
                    brandFullVM.ProductVMs.Add(listProduct[j]);
                }
            }

            var brandImgBLL = new BrandImageBLL();
            var listImg = await brandImgBLL.GetByBrandId(brandFullVM.Id);
            if (listImg.Count > 0)
            {
                brandFullVM.BrandImageVMs = new List<BrandImageVM>();
                for (int i = 0; i < listImg.Count; i++)
                {
                    brandFullVM.BrandImageVMs.Add(listImg[i]);
                }
            }

            return brandFullVM;
        }
        
    }
}
