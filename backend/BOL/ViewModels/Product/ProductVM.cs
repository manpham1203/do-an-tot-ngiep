using BOL.ViewModels.Brand;
using BOL.ViewModels.Category_Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.ViewModels.Product
{
    public class ProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public string BrandId { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public BrandVM Brand { get; set; }
        public IList<CategoryVM_ProductVM> CategoryVM_ProductVMs { get; set; }
    }
}
