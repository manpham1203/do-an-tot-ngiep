using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class ProductComment
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public float Rating { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
