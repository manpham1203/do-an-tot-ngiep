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
        public decimal? From { get;set; }
        public decimal? To { get;set; }
        public string ShortBy { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int Limit { get; set; } = 10;
        public List<string> CategorySlug { get; set; }
        public List<string> BrandSlug { get; set; }
    }
}
