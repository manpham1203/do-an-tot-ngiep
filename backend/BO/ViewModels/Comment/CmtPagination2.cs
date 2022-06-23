using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Comment
{
    public class CmtPagination2
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<CmtRowAminVM> List { get; set; }
    }
}
