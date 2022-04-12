using BO.ViewModels.Post;
using DAL.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Post
{
    public class PostBLL
    {
        private readonly PostDAL postDAL;
        private CommonBLL cm;
        public PostBLL()
        {
            postDAL = new PostDAL();
        }
        public async Task<List<PostVM>> GetAll()
        {
            return await postDAL.GetAll();
        }
        public async Task<PostVM> GetById(string id)
        {
            if (id.Length != 12)
            {
                return null;
            }
            var postVM = await postDAL.GetById(id);
            if(postVM == null)
            {
                return null;
            }
            return postVM;
        }
        public async Task<bool> Create(CreatePostVM model)
        {
            cm = new CommonBLL();
            var postId = cm.RandomString(9);
            var checkExists=await GetById(postId);
            while (checkExists != null)
            {
                postId=cm.RandomString(9);
                checkExists=await GetById(postId);
            }
            postId = "POT" + postId;
            var slug = Regex.Replace(cm.RemoveUnicode(model.Title).Trim().ToLower(), @"\s+", "-");
            var postVM = new PostVM
            {
                Id = postId,
                Title = model.Title,
                Slug = slug,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                Published = model.Published,
                Deleted = false,
                Likes = 0,
                Views = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
            };
            return await postDAL.Create(postVM);
        }
        public async Task<bool> Update(string id, UpdatePostVM model)
        {
            cm = new CommonBLL();

            var checkExists = await GetById(id);
            if (checkExists == null)
            {
                return false;
            }
            var slug = Regex.Replace(cm.RemoveUnicode(model.Title).Trim().ToLower(), @"\s+", "-");
            var postVM = new PostVM
            {
                Id = id,
                Title = model.Title,
                Slug = slug,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                Published = model.Published,
                Deleted = model.Deleted,
                Likes = model.Likes,
                Views = model.Views,
                UpdatedAt = DateTime.Now,
            };
            return await postDAL.Update(postVM);
        }
        public async Task<bool> Delete(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var checkExists = await GetById(id);
            if (checkExists == null)
            {
                return false;
            }
            return await postDAL.Delete(id);
        }
    }
}
