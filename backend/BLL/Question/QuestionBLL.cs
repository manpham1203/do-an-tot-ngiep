using BO.ViewModels.Contact;
using DAL.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contact
{
    public class QuestionBLL
    {
        private QuestionDAL questionDAL;
        private CommonBLL cm;
        public QuestionBLL()
        {
            questionDAL = new QuestionDAL();
        }
        public async Task<bool> CheckExists(string id)
        {
            try
            {
                return await questionDAL.CheckExists(id);
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Create(QuestionVM model)
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
                return await questionDAL.Create(model);
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<QuestionVM>> GetAll()
        {
            try
            {
                return await questionDAL.GetAll();
            }
            catch
            {
                return null;
            }
        }
        public async Task<QuestionPaginationVM> QuestionPagination(int currentPage, int limit, string email, string name, string content)
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
                    return new QuestionPaginationVM
                    {
                        TotalPage = 0,
                        TotalResult = 0,
                        QuestionVMs = new List<QuestionVM>(),
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
                return new QuestionPaginationVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    QuestionVMs = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }
    
        public async Task<QuestionPaginationVM> QuestionToday(int currentPage, int limit, string email, string name, string content)
        {
            try
            {
                var resultFromDAL = await questionDAL.QuestionToday();
                if (resultFromDAL == null)
                {
                    return null;
                }
                if (resultFromDAL.Count == 0)
                {
                    return new QuestionPaginationVM
                    {
                        TotalPage = 0,
                        TotalResult = 0,
                        QuestionVMs = new List<QuestionVM>(),
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
                return new QuestionPaginationVM
                {
                    TotalResult = count,
                    TotalPage = totalPage,
                    QuestionVMs = resultFromDAL,
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
