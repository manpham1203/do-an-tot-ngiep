using BOL.ViewModels.Category;
using BOL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.ViewModels.Category_Product
{
    public class CategoryVM_ProductVM
    {
        public string CategoryId { get; set; }
        public CategoryVM CategoryVM { get; set; }
        public string ProductId { get; set; }
        public ProductVM ProductVM { get; set; }
    }
}
