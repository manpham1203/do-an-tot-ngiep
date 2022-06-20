using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Brand
{
    public class BrandPaginationAdminVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<BrandNameVM> Brands { get; set; }
    }
}
