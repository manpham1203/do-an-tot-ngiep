using BO.ViewModels.BrandImage;
using DAL.BrandImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BrandImage
{
    public class BrandImageBLL
    {
        private BrandImageDAL brandImageDAL;
        private CommonBLL cm;

        public BrandImageBLL()
        {
            brandImageDAL = new BrandImageDAL();
        }
        public async Task<BrandImageVM> GetById(string id)
        {
            return await brandImageDAL.GetById(id);
        }
        public async Task<List<BrandImageVM>> GetByBrandId(string id)
        {
            return await brandImageDAL.GetByBrandId(id);
        }
        public async Task<bool> Create(List<string> imgName, string brandId)
        {
            cm = new CommonBLL();
            List<BrandImageVM> brandImages = new List<BrandImageVM>();
            BrandImageVM brandImageVM;
            for(int i = 0; i < imgName.Count; i++)
            {
                var imgId = cm.RandomString(12);
                var checkImg = await GetById(imgId);
                while (checkImg != null)
                {
                    imgId = cm.RandomString(12);
                    checkImg = await GetById(brandId);
                }
                brandImageVM = new BrandImageVM
                {
                    Id = imgId,
                    BrandId = brandId,
                    Name = imgName[i],
                    Published = true,
                };
                brandImages.Add(brandImageVM);
            }
            return await brandImageDAL.Create(brandImages);
        }
        public async Task<bool> Delete(string id)
        {
            var brandImg = await GetById(id);
            if (brandImg == null)
            {
                return false;
            }
            return await brandImageDAL.Delete(id);
        }
        public async Task<bool> Published(string id)
        {
            var brandImageVM = await GetById(id);
            if (brandImageVM == null)
            {
                return false;
            }
            bool published = !brandImageVM.Published;
            var result = await brandImageDAL.Pulished(id, published);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
