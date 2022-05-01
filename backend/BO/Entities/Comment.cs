using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Entities
{
    public class Comment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string OrderDetailId { get; set; }
        public int? Star { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ParentId { get; set; }
        public Comment Parent { get; set; }
        public List<Comment> Children { get; set; }
        public User User { get; set; }
        //public Post Post { get;set; }
        //public Product Product { get; set; }
        //public OrderDetail OrderDetail { get; set; }
    }
}
