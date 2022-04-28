using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Comment
{
    public class ProductCmtPaginationVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<string> List { get; set; }
    }
}
