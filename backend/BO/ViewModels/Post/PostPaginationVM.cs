using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Post
{
    public class PostPaginationVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<string> posts { get; set; }
    }
}
