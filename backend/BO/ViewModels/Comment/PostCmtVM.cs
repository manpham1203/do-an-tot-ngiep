using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Comment
{
    public class PostCmtVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Published { get; set; }
        public string ParentId { get; set; }
        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
        public string FullName { get; set; }
        public List<PostCmtVM> Children { get; set; }
    }
}
