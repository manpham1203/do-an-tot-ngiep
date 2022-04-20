using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Wishlist
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
