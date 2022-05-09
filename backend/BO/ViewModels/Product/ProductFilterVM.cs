using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class ProductFilterVM
    {
        public string Search { get; set; }
        public string PriceRange { get;set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public string OrderBy { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public List<string> CategorySlugs { get; set; }
        public List<string> BrandSlugs { get; set; }
        public bool? Discount { get; set; }
    }
}
