using BO.ViewModels.Category;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Product
{
    public class CreateProductVM
    {

        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public bool Published { get; set; }

        public string BrandId { get; set; }
        public List<string> CategoryIds { get; set; }

        public List<string> ImageNames { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
