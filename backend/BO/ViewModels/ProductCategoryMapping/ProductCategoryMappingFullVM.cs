using BO.ViewModels.Category;
using BO.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.ProductCategoryMapping
{
    public class ProductCategoryMappingFullVM
    {
        public string CategoryId { get; set; }
        public CategoryFullVM CategoryFullVM { get; set; }
        public string ProductId { get; set; }
        public ProductFullVM ProductFullVM { get; set; }
    }
}
