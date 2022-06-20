using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.ViewModels.Banner
{
    public class BannerVM
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string SubContent { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int Order { get; set; }
        public string LinkTo { get; set; }

        public string ImageName { get; set; }
        public string ImageSrc { get; set; }
    }
}
