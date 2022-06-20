using BO.ViewModels.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class CartRowVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal? CurrentPrice { get; set; }
        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
        public string BrandId { get; set; }
        public BrandNameVM BrandNameVM { get; set; }

    }
}
