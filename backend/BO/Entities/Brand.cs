using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Brand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public bool IsActive { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Ordinal { get; set; }
        public IList<Product> Products { get; set; }
    }
}
