using BLL.Picture;
using BO.ViewModels.Picture;
using BO.ViewModels.Post;
using DAL.Post;
using System;
using System.Collections.Generic;
using System.IO;
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
        private string objectType="post";
        private string regex = @"[`!@#$%^&*()_+|\-=\\{}\[\]:"";'<>?,./]";
        public PostBLL()
        {
            postDAL = new PostDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await postDAL.CheckExists(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(CreatePostVM model)
        {
            cm = new CommonBLL();
            var postId = cm.RandomString(12);
            var checkExists = await CheckExists(postId);
            while (checkExists == true)
            {
                postId = cm.RandomString(9);
                checkExists = await CheckExists(postId);
            }
            var slug = Regex.Replace(model.Title, regex, string.Empty);
            slug = Regex.Replace(cm.RemoveUnicode(slug).Trim().ToLower(), @"\s+", "-");

            if (model.File != null)
            {
                string imageName = slug;
                imageName += DateTime.Now.ToString("yyMMddHHmmssfff") + Path.GetExtension(model.File.FileName);
                model.ImageName = imageName;
            }


            var postVM = new PostVM
            {
                Id = postId,
                Title = model.Title,
                Slug = slug,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                Published = model.Published,
                Deleted = false,
                View = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
            };

            var pictureBLL = new PictureBLL();
            var pictureId = cm.RandomString(16);
            var checkPictureId = await pictureBLL.CheckExists(pictureId);
            while (checkPictureId)
            {
                pictureId = cm.RandomString(16);
                checkPictureId = await pictureBLL.CheckExists(pictureId);
            }

            var pictureVM = new PictureVM
            {
                Id = pictureId,
                Name = model.ImageName,
                ObjectId = postId,
                Published = true,
                ObjectType= objectType,
            };

            return await postDAL.Create(postVM, pictureVM);
        }
        public async Task<bool> Update(string id, UpdatePostVM model)
        {
            cm = new CommonBLL();

            var checkExists = await CheckExists(id);
            if (checkExists == false)
            {
                return false;
            }
            var slug = Regex.Replace(model.Title, regex, string.Empty);
            slug = Regex.Replace(cm.RemoveUnicode(slug).Trim().ToLower(), @"\s+", "-");


            if (model.File != null)
            {
                string imageName = slug;
                imageName +=Path.GetExtension(model.File.FileName);
                model.ImageName = imageName;
            }

            var postVM = new PostVM
            {
                Id = id,
                Title = model.Title,
                Slug = slug,
                ShortDescription = model.ShortDescription,
                FullDescription = model.FullDescription,
                Published = model.Published,
                Deleted = model.Deleted,
                View = model.View,
                UpdatedAt = DateTime.Now,
            };

            var pictureVM = new PictureVM
            {
                Name = model.ImageName,
                ObjectId = id,
                Published = true,
            };

            return await postDAL.Update(postVM, pictureVM);
        }
        public async Task<bool> Delete(string id)
        {
            if (id.Length != 12)
            {
                return false;
            }
            var checkExists = await CheckExists(id);
            if (checkExists == false)
            {
                return false;
            }
            return await postDAL.Delete(id);
        }
    
        public async Task<PostPaginationVM> RowsAdminDeleted(bool deleted, string query, int limit = 10, int currentPage = 1)
        {
            try
            {
                var resultFromDAL = await postDAL.RowsAdminDeleted(deleted, query);
                if (resultFromDAL == null)
                {
                    return null;
                }

                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();

                var result = new PostPaginationVM
                {
                    TotalPage = totalPage,
                    posts = resultFromDAL,
                    TotalResult = count,
                };
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<RowAdmin> RowAdminById(string id)
        {
            try
            {
                return await postDAL.RowsAdminById(id);
            }
            catch
            {
                return null;
            }
        }
    
        public async Task<bool> Published(string id)
        {
            try
            {
                return await postDAL.Published(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Deleted(string id)
        {
            try
            {
                return await postDAL.Deleted(id);
            }
            catch
            {
                return false;
            }
        }
    
        public async Task<SetDataUpdateVM> SetDataUpdate(string id)
        {
            try
            {
                return await postDAL.SetDataUpdate(id);
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> DeleteFromDatabase(string id)
        {
            try
            {
                return await postDAL.DeleteFromDatabase(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<PostCardVM>> PostCards(int limit, int currentPage)
        {
            try
            {
                return await postDAL.PostCards();
                
            }
            catch
            {
                return null;
            }
        }
        public async Task<PostPaginationClientVM> PostPagination(int limit, int currentPage)
        {
            try
            {
                var resultFromDAL = await postDAL.PostCards();
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new PostPaginationClientVM
                    {
                        TotalResult = 0,
                        TotalPage = 0,
                        PostCardVMs = new List<PostCardVM>(),
                    };
                }
                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();
                return new PostPaginationClientVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    PostCardVMs = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }
        public async Task<PostDetailVM> PostDetail(string slug)
        {
            try
            {
                return await postDAL.PostDetail (slug);
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
                return await postDAL.IncreaseView(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> PublishedTrueList(List<string> ids)
        {
            try
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await postDAL.PublishedTrueList(ids);
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
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await postDAL.PublishedFalseList(ids);
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
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await postDAL.DeletedTrueList(ids);
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
                    var checkExists = await CheckExists(ids[i]);
                    if (checkExists == false)
                    {
                        return false;
                    }
                }
                return await postDAL.DeletedFalseList(ids);
            }
            catch
            {
                return false;
            }
        }
        public async Task<PostDetailVM> PostDetailId(string id)
        {
            try
            {
                return await postDAL.PostDetailId(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
