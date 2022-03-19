using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class UpdateProductVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public string BrandId { get; set; }
        public IList<string> ListCategoryId { get; set; }
    }
}
