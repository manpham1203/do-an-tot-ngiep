using BO.ViewModels.Comment;
using DAL.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Comment
{
    public class PostCmtBLL
    {
        private PostCmtDAL postCmtDAL;
        private CommonBLL cm;
        public PostCmtBLL()
        {
            postCmtDAL = new PostCmtDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await postCmtDAL.CheckExists(id);
            }
            catch
            {
                return true;
            }
        }
        public async Task<bool> Create(PostCmtVM model)
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
                model.Content = string.IsNullOrEmpty(model.Content) ? null : model.Content;
                model.Id = cmtId;
                model.ObjectType = "post";
                model.CreatedAt = DateTime.Now;
                model.ParentId = null;
                model.Published = true;
                return await postCmtDAL.Create(model);
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<PostCmtVM>> CmtChildren(string parentId)
        {
            try
            {
                return await postCmtDAL.CmtChildren(parentId);
            }
            catch
            {
                return null;
            }
        }
        public async Task<PostCmtVM> CommentItem(string id)
        {
            try
            {
                var resultFromDAL = await postCmtDAL.CommentItem(id);
                var children = await CmtChildren(id);
                resultFromDAL.Children = children;
                return resultFromDAL;
            }
            catch
            {
                return null;
            }
        }
        public async Task<CmtPagination> IdsOfPost(string postId, int limit, int currentPage)
        {
            try
            {
                var resultFromDAL = await postCmtDAL.IdsOfPost(postId);
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
                var cmtVM = new PostCmtVM();
                cmtVM.Content = content;
                cmtVM.Id = cmtId;
                cmtVM.ObjectType = "product";
                cmtVM.CreatedAt = DateTime.Now;
                cmtVM.ParentId = parentId;
                cmtVM.ObjectId = objectId.ObjectId;
                cmtVM.UserId = userId;
                cmtVM.Published = true;
                return await postCmtDAL.Create(cmtVM);
            }
            catch
            {
                return false;
            }
        }
    }
}
