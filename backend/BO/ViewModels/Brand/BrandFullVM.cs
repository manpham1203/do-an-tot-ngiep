using BO.ViewModels.Picture;
using BO.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Brand
{
    public class BrandFullVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<ProductVM> ProductVMs { get; set; }
        public List<ProductFullVM> ProductFullVMs { get; set; }
        public PictureVM PictureVM { get; set; }
    }
}
