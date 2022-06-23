using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class ProductPaginationAdminVM
    {
        public int TotalPage { get; set; }
        public int TotalResult { get; set; }
        public List<ProductNameVM> Products { get; set; }
    }
}
