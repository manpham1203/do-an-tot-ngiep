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
                cm=new CommonBLL();
                model.Id = cm.RandomString(12);
                var checkExists=await CheckExists(model.Id);
                if (checkExists)
                {
                    model.Id = cm.RandomString(12);
                    checkExists = await CheckExists(model.Id);
                }
                model.CreatedAt = DateTime.Now;
                return await contactDAL.Create(model);
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<ContactVM>> GetAll()
        {
            try
            {
                return await contactDAL.GetAll();
            }
            catch
            {
                return null;
            }
        }
        public async Task<ContactPaginationVM> ContactPagination(int currentPage, int limit, string email, string name, string content)
        {
            try
            {
                var resultFromDAL = await GetAll();
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
                        ContactVMs = new List<ContactVM>(),
                    };
                }

                if (!string.IsNullOrEmpty(email))
                {
                    resultFromDAL =resultFromDAL.Where(x=>x.Email.ToLower().Contains(email.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    resultFromDAL = resultFromDAL.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(content))
                {
                    resultFromDAL = resultFromDAL.Where(x => x.Content.ToLower().Contains(content.ToLower())).ToList();
                }

                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();
                return new ContactPaginationVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    ContactVMs = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }
    
        public async Task<ContactPaginationVM> ContactToday(int currentPage, int limit, string email, string name, string content)
        {
            try
            {
                var resultFromDAL = await contactDAL.ContactToday();
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
                        ContactVMs = new List<ContactVM>(),
                    };
                }

                if (!string.IsNullOrEmpty(email))
                {
                    resultFromDAL = resultFromDAL.Where(x => x.Email.ToLower().Contains(email.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(name))
                {
                    resultFromDAL = resultFromDAL.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                if (!string.IsNullOrEmpty(content))
                {
                    resultFromDAL = resultFromDAL.Where(x => x.Content.ToLower().Contains(content.ToLower())).ToList();
                }

                var count = resultFromDAL.Count();
                var totalPage = (int)Math.Ceiling(count / (double)limit);
                resultFromDAL = resultFromDAL.Skip((currentPage - 1) * limit).Take(limit).ToList();
                return new ContactPaginationVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    ContactVMs = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
