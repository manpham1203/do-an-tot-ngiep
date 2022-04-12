using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using BO.ViewModels.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class ProductFullVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public int Quantity { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string BrandId { get; set; }
        public BrandVM BrandVM { get; set; }
        public List<CategoryVM> CategoryVMs { get; set; }
        public List<ProductImageVM> ProductImageVMs { get; set; }
    }
}
