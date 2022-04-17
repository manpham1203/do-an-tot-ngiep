using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Category
{
    public class CategoryPaginationAdminVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<CategoryNameVM> Categories { get; set; }
    }
}
