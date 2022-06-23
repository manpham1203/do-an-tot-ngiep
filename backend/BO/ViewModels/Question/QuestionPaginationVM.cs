using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Contact
{
    public class QuestionPaginationVM
    {
        public int TotalResult { get;set; }
        public int TotalPage { get; set; }
        public List<QuestionVM> QuestionVMs { get; set; }
    }
}
