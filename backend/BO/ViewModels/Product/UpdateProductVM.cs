using Microsoft.AspNetCore.Http;
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
        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public string BrandId { get; set; }
        public List<string> CategoryIds { get; set; }
        public List<string> ImageNames { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
