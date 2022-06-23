using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Picture
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Published { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public int Order { get; set; }
        //public Product Product { get; set; }
        //public Post Post { get; set; }
        //public Brand Brand { get; set; }
        //public Category Category { get; set; }
        //public User User { get; set; }
    }
}
