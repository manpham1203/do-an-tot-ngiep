using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.CategoryImage
{
    public class CategoryImageVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public bool Pulished { get; set; }
        public string ImageSrc { get; set; }
    }
}
