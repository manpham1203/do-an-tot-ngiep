using BLL.Product;
using BO.ViewModels.Brand;
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
            if (brandFullVMs == null)
            {
                return null;
            }
            var productBLL = new ProductBLL();
            for (int i = 0; i < brandFullVMs.Count; i++)
            {
                var listProduct = await productBLL.GetByBrandId(brandFullVMs[i].Id);
                if (listProduct == null)
                {
                    brandFullVMs[i].ProductVMs = null;
                }
                else
                {
                    brandFullVMs[i].ProductVMs = new List<ProductVM>();
                    for (int j = 0; j < listProduct.Count; j++)
                    {
                        brandFullVMs[i].ProductVMs.Add(listProduct[j]);
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
            if (listProduct == null)
            {
                brandFullVM.ProductVMs = null;
            }
            else
            {
                brandFullVM.ProductVMs = new List<ProductVM>();
                for (int j = 0; j < listProduct.Count; j++)
                {
                    brandFullVM.ProductVMs.Add(listProduct[j]);
                }
            }


            return brandFullVM;
        }
    }
}
