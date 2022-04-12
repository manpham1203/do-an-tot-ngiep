using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class ProductImage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }
        public bool Published { get; set; }
        public Product Product { get; set; }
    }
}
