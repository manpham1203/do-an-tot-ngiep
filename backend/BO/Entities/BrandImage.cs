using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class BrandImage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BrandId { get; set; }
        public bool Published { get; set; }
        public Brand Brand { get; set; }
    }
}
