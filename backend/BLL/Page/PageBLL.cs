
using BO.ViewModels.Page;
using DAL.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Page
{
    public class PageBLL
    {
        private PageDAL pageDAL;
        private CommonBLL cm;
        private string regex = @"[`!@#$%^&*()_+|\-=\\{}\[\]:"";'<>?,./]";
        public PageBLL()
        {
            pageDAL = new PageDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await pageDAL.CheckExists(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(PageVM model)
        {
            try
            {
                cm = new CommonBLL();
                var id = cm.RandomString(6);
                var checkExists = await CheckExists(id);
                if (checkExists)
                {
                    id = cm.RandomString(6);
                    checkExists = await CheckExists(id);
                }
                var slug = Regex.Replace(model.Title, regex, string.Empty);
                model.Slug = Regex.Replace(cm.RemoveUnicode(slug).Trim().ToLower(), @"\s+", "-");
                model.Id = id;
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = null;
                return await pageDAL.Create(model);
            }
            catch
            {
                return false;
            }            
        }

        public async Task<List<string>> GetListId(bool deleted, bool published)
        {
            try
            {
                return await pageDAL.GetListId(deleted);
            }
            catch
            {
                return null;
            }
        }
        public async Task<PagePaginationVM> PagePagination(bool deleted, int limit, int currentPage)
        {
            try
            {
                var resultFromDAL = await pageDAL.GetListId(deleted);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new PagePaginationVM
                    {
                        TotalPage = 0,
                        TotalResult = 0,
                        Data = new List<string>(),
                    };
                }
                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();
                return new PagePaginationVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    Data = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<PageVM> GetById(string id)
        {
            try
            {
                return await pageDAL.GetById(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }

                return await pageDAL.Delete(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Published(string id)
        {
            try
            {
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }

                return await pageDAL.Published(id);
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
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }

                return await pageDAL.Deleted(id);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, PageVM model)
        {
            try
            {
                cm = new CommonBLL();
                var checkExists = await CheckExists(id);
                if (checkExists == false)
                {
                    return false;
                }
                model.Id = id;
                var slug = Regex.Replace(model.Title, regex, string.Empty);
                model.Slug = Regex.Replace(cm.RemoveUnicode(slug).Trim().ToLower(), @"\s+", "-");
                model.UpdatedAt = DateTime.Now;

                return await pageDAL.Update(model);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<PageVM>> GetListByType(string type)
        {
            try
            {
                return await pageDAL.GetListByType(type);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<PageFooterVM>> GetList()
        {
            try
            {
                var list = new List<PageFooterVM>();
                var listPolicy = await GetListByType("policy");
                var listGuide = await GetListByType("guide");
                if (listPolicy != null)
                {
                    if (listPolicy.Count > 0)
                    {
                        var temp = new List<PageFooterVM>();
                        temp = listPolicy.Select(x => new PageFooterVM
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Slug = x.Slug,
                        }).ToList();
                        list.AddRange(temp);
                    }
                }
                if (listGuide != null)
                {
                    if (listGuide.Count > 0)
                    {
                        var temp = new List<PageFooterVM>();
                        temp = listGuide.Select(x => new PageFooterVM
                        {
                            Id = x.Id,
                            Title = x.Title,
                            Slug = x.Slug,
                        }).ToList();
                        list.AddRange(temp);
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
        public async Task<PageVM> GetBySlug(string slug)
        {
            try
            {
                return await pageDAL.GetBySlug(slug);
            }
            catch
            {
                return null;
            }
        }
    }
}
