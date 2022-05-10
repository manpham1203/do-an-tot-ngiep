using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.User
{
    public class UserPaginationVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<UserVM> Data { get; set; }
    }
}
