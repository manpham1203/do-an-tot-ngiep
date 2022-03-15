using BOL.ViewModels.Category;
using BOL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.ViewModels.ProductCategoryMapping
{
    public class ProductCategoryMappingFullVM
    {
        public string CategoryId { get; set; }
        public CategoryFullVM CategoryFullVM { get; set; }
        public string ProductId { get; set; }
        public ProductFullVM ProductFullVM { get; set; }
    }
}
