using BO.ViewModels.Contact;
using DAL.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contact
{
    public class ContactBLL
    {
        private ContactDAL contactDAL;
        private CommonBLL cm;
        public ContactBLL()
        {
            contactDAL = new ContactDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await contactDAL.CheckExists(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(ContactVM model)
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
                model.Id = id;
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = null;
                model.Deleted = false;
                return await contactDAL.Create(model);
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
                return await contactDAL.GetListId(deleted);
            }
            catch
            {
                return null;
            }
        }
        public async Task<ContactPaginationVM> ContactPagination(bool deleted, int limit, int currentPage)
        {
            try
            {
                var resultFromDAL = await contactDAL.GetListId(deleted);
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new ContactPaginationVM
                    {
                        TotalPage = 0,
                        TotalResult = 0,
                        Data = new List<string>(),
                    };
                }
                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();
                return new ContactPaginationVM
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

        public async Task<ContactVM> GetById(string id)
        {
            try
            {
                return await contactDAL.GetById(id);
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

                return await contactDAL.Delete(id);
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

                return await contactDAL.Published(id);
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

                return await contactDAL.Deleted(id);
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, ContactVM model)
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
                model.UpdatedAt = DateTime.Now;

                return await contactDAL.Update(model);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ContactVM>> GetListByType(string type)
        {
            try
            {
                return await contactDAL.GetListByType(type);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ContactFooterVM>> GetList()
        {
            try
            {
                var list = new List<ContactFooterVM>();
                var listNumber = await GetListByType("number");
                var listEmail = await GetListByType("email");
                var listAddress = await GetListByType("address");
                if (listNumber != null)
                {
                    if (listNumber.Count > 0)
                    {
                        var temp = new List<ContactFooterVM>();
                        temp = listNumber.Select(x => new ContactFooterVM
                        {
                            Id = x.Id,
                            Content = x.Content,
                            Name = "Điện thoại"
                        }).ToList();
                        list.AddRange(temp);
                    }
                }
                if (listEmail != null)
                {
                    if (listEmail.Count > 0)
                    {
                        var temp = new List<ContactFooterVM>();
                        temp = listEmail.Select(x => new ContactFooterVM
                        {
                            Id = x.Id,
                            Content = x.Content,
                            Name = "Email"
                        }).ToList();
                        list.AddRange(temp);
                    }
                }
                if (listAddress != null)
                {
                    if (listAddress.Count > 0)
                    {
                        var temp = new List<ContactFooterVM>();
                        temp = listAddress.Select(x => new ContactFooterVM
                        {
                            Id = x.Id,
                            Content = x.Content,
                            Name = "Địa chỉ"
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
    }
}
