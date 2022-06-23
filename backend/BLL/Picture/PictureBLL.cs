using BO.ViewModels.Picture;
using DAL.Picture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Picture
{
    public class PictureBLL
    {
        private PictureDAL pictureDAL;
        private CommonBLL cm;
        public PictureBLL()
        {
            pictureDAL = new PictureDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await pictureDAL.CheckExists(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(List<string> imgName, string objectId)
        {
            cm = new CommonBLL();
            List<PictureVM> pictureVMs = new List<PictureVM>();
            PictureVM pictureVM;
            for (int i = 0; i < imgName.Count; i++)
            {
                var imgId = cm.RandomString(12);
                var checkImg = await CheckExists(imgId);
                while (checkImg)
                {
                    imgId = cm.RandomString(12);
                    checkImg = await CheckExists(imgId);
                }
                pictureVM = new PictureVM
                {
                    Id = imgId,
                    ObjectId = objectId,
                    Name = imgName[i],
                    Published = true,
                };
                pictureVMs.Add(pictureVM);
            }
            return await pictureDAL.Create(pictureVMs);
        }
        public async Task<bool> Delete(string id)
        {
            var productImage = await CheckExists(id);
            if (productImage == false)
            {
                return false;
            }
            return await pictureDAL.Delete(id);
        }
        public async Task<PictureVM> GetById(string id)
        {
            try
            {
                return await pictureDAL.GetById(id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> Published(string id)
        {
            var productImageVM = await GetById(id);
            if (productImageVM == null)
            {
                return false;
            }
            bool pulished = !productImageVM.Published;
            var result = await pictureDAL.Pulished(id, pulished);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<List<PictureVM>> GetByObjectId(string objectId, string objectType)
        {
            try
            {
                return await pictureDAL.GetByObjectId(objectId, objectType);
            }
            catch
            {
                return null;
            }
        }
    }
}
