using BO.ViewModels.Brand;
using BO.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class ProductRowAdminVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BrandId { get; set; }
        public BrandNameVM BrandNameVM { get; set; }
        public List<CategoryNameVM> CategoryNameVMs { get; set; }
        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
    }
}
