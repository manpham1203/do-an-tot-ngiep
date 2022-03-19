using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string BrandId { get; set; }
        public Brand Brand { get; set; }
        public IList<Product_Category_Mapping> Product_Category_Mapping { get; set; }
    }
}
