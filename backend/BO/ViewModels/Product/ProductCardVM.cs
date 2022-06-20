using BO.ViewModels.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class ProductCardVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public BrandNameVM BrandNameVM { get; set; }
        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
        public string BrandId { get; set; }
        public string QuantityInStock { get; set; }
        public float? Star { get; set; }
    }
}
